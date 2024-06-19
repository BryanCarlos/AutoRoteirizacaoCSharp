using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Text;

namespace AutoRoteirizacao
{
    public partial class frmAutoRoteirizacao : Form
    {
        public frmAutoRoteirizacao()
        {
            InitializeComponent();
        }

        private void btnSelectMaterialList_Click(object sender, EventArgs e)
        {
            string userName = Environment.UserName;
            using OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Excel.Application xlApp = new Excel.Application();
                try
                {
                    string filePath = openFileDialog.FileName;
                    Match fileName = Regex.Match(filePath, @"[^\\]+$");

                    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
                    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                    Excel.Range xlRange = xlWorksheet.UsedRange;

                    int rowCount = xlRange.Rows.Count;
                    int colCount = xlRange.Columns.Count;

                    List<Peca> pecas = new List<Peca>();

                    for (int i = 3; i < rowCount; i++)
                    {
                        if (xlRange.Cells[4][i].Value2 != null && xlRange.Cells[4][i].Value2.ToString().StartsWith("30"))
                        {
                            pecas.Add(new Peca(xlRange.Cells[2][i].Value2.ToString(), xlRange.Cells[3][i].Value2.ToString(), xlRange[4][i].Value2.ToString(), NormalizeString(xlRange[6][i].Value2.ToString())));
                        }
                        else
                        {
                            continue;
                        }
                    }


                    // Criando a pasta de trabalho e planilhas de saída
                    var resultExcel = new Excel.Application();
                    resultExcel.Visible = true;
                    var resultWorkbook = resultExcel.Workbooks.Add();
                    resultWorkbook.Sheets.Add(After: resultWorkbook.Sheets[resultWorkbook.Sheets.Count], Count: 4);
                    string[] sheets = { "GUILHOTINA", "PLASMA", "LASER", "SERRA", "SEMOBS" };
                    string[] headers = { "CODIGO", "OBSERVACOES", "DESCRICAO", "QTD" };

                    for (int i = 0; i < sheets.Length; i++)
                    {
                        resultWorkbook.Sheets[i + 1].Name = sheets[i];
                    }

                    var guilhotinaSheet = resultWorkbook.Sheets["GUILHOTINA"];
                    guilhotinaSheet.Range["A1:D1"].Value2 = headers;

                    var plasmaSheet = resultWorkbook.Sheets["PLASMA"];
                    plasmaSheet.Range["A1:D1"].Value2 = headers;

                    var laserSheet = resultWorkbook.Sheets["LASER"];
                    laserSheet.Range["A1:D1"].Value2 = headers;

                    var serraSheet = resultWorkbook.Sheets["SERRA"];
                    serraSheet.Range["A1:D1"].Value2 = headers;

                    var semObsSheet = resultWorkbook.Sheets["SEMOBS"];
                    semObsSheet.Range["A1:D1"].Value2 = headers;

                    int rowGuilhotina = 2, rowPlasma = 2, rowLaser = 2, rowSerra = 2, rowSemObs = 2;

                    foreach (Peca peca in pecas)
                    {
                        object[] pecaData = { peca.codigo, peca.observacoes, peca.descricao, peca.qtd };
                        switch (peca.observacoes)
                        {
                            case "G M":
                            case "G D M":
                            case "G D S M":
                            guilhotinaSheet.Range[$"A{rowGuilhotina}:D{rowGuilhotina}"].Value2 = pecaData;
                            rowGuilhotina++;
                            break;
                            case "P M":
                            case "P D M":
                            case "P U M":
                            plasmaSheet.Range[$"A{rowPlasma}:D{rowPlasma}"].Value2 = pecaData;
                            rowPlasma++;
                            break;
                            case "L M":
                            case "L D M":
                            case "L U M":
                            laserSheet.Range[$"A{rowLaser}:D{rowLaser}"].Value2 = pecaData;
                            rowLaser++;
                            break;
                            case "S M":
                            case "S D M":
                            case "S U M":
                            serraSheet.Range[$"A{rowSerra}:D{rowSerra}"].Value2 = pecaData;
                            rowSerra++;
                            break;
                            default:
                            semObsSheet.Range[$"A{rowSemObs}:D{rowSemObs}"].Value2 = pecaData;
                            rowSemObs++;
                            break;
                        }
                    }

                    foreach (var sheet in new[] { guilhotinaSheet, plasmaSheet, laserSheet, serraSheet, semObsSheet })
                    {
                        sheet.Columns.AutoFit();
                        sheet.Rows.AutoFit();
                    }

                    resultWorkbook.SaveAs($"C:\\Users\\{userName}\\Downloads\\roteirizado - {fileName}");
                } 
                finally
                {
                    xlApp.Quit();
                }
            }
        }

        public static string NormalizeString(string str)
        {
            // Mapeamento de substituição de números para letras
            Dictionary<char, char> numberToLetter = new Dictionary<char, char>
            {
                {'0', 'C'},
                {'1', 'S'},
                {'2', 'G'},
                {'3', 'P'},
                {'4', 'U'},
                {'5', 'D'},
                {'6', 'P'},
                {'7', 'D'},
                {'8', 'U'},
                {'9', ' '}
            };

            // Expressão regular para encontrar padrões de entrada
            string pattern = @"[A-Za-z0-9]+";
            MatchCollection matches = Regex.Matches(str, pattern);

            StringBuilder result = new StringBuilder();

            foreach (Match match in matches)
            {
                foreach (char c in match.Value)
                {
                    if (char.IsDigit(c) && numberToLetter.ContainsKey(c))
                    {
                        result.Append(numberToLetter[c] + " ");
                    }
                    else
                    {
                        result.Append(c + " ");
                    }
                }
            }

            result.Append("M");
            return result.ToString().ToUpper().TrimEnd();
        }
    }
}
