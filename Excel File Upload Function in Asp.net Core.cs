 public static long ExtractAndSaveData(string fileType, Stream fileStream)
        {
            if (fileType == "excel")
            {
                using var package = new ExcelPackage(fileStream);
                var worksheet = package.Workbook.Worksheets[0];

                for (int row = 2; row <= worksheet.Dimension.Rows; row++) // Assuming headers are in the first row
                {
                    var values = new List<string>();
                    for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                    {
                        /*var cellValue = worksheet.Cells[row, col].Value;
                        var res = cellValue?.ToString() == "NULL" ? 0.ToString() : cellValue?.ToString();
                        //var valueString = cellValue?.ToString() ?? string.Empty;
                        values.Add(res);*/

                        values.Add(worksheet.Cells[row, col].Value?.ToString());
                    }
                    int x = 0;

                    var excelData = new ExcelDataModel
                    {
                        Id = Convert.ToInt64(values[0]),
                        LeadDate = Convert.ToDateTime(values[1] == "NULL" ? DateTime.MinValue : values[1]),
                        LeadSourceId = Convert.ToInt64(values[2] == "NULL" ? 0 : values[2]),
                        SourceNameId = Convert.ToInt64(values[3] == "NULL" ? 0 : values[3]),
                        UserName = values[4],                        // Added
                        LeadStatusId = Convert.ToInt64(values[5] == "NULL" ? 0 : values[5]),
                        MobileNo = values[6],                        // Added
                        Email = values[7],                           // Added
                        Note = values[8],                            // Added
                        LeadOwnerId = Convert.ToInt64(values[9] == "NULL" ? 0 : values[9]),
                        StatusId = Convert.ToInt64(values[10] == "NULL" ? 0 : values[10]),      // Added
                        CreatedBy = Convert.ToInt64(values[11] == "NULL" ? 0 : values[11]),     // Added
                        CreatedDate = Convert.ToDateTime(values[12] == "NULL" ? DateTime.MinValue : values[12]), // Added
                        ModifiedBy = Convert.ToInt64(values[13] == "NULL" ? 0 : values[13]),    // Added
                        ModifiedDate = Convert.ToDateTime(values[14] == "NULL" ? DateTime.MinValue : values[14]),// Added
                        IsDeleted = false,    // Added   values[] == "NULL" ? 0 : values[]
                        SourceTypeId = Convert.ToInt64(values[16] == "NULL" ? 0 : values[16]),  // Added
                        UserTypeId = Convert.ToInt64(values[17] == "NULL" ? 0 : values[17])     // Added
                    };

                    //dbContext.ExcelData.Add(excelData);                    
                }
                return 1;
            }
            else if (fileType == "csv")
            {
                using var parser = new TextFieldParser(fileStream)
                {
                    TextFieldType = FieldType.Delimited,
                    Delimiters = new[] { "," },
                    HasFieldsEnclosedInQuotes = true
                };
                bool firstRow = true;
                while (!parser.EndOfData)
                {
                    if (firstRow)
                    {
                        firstRow = false;
                        continue;
                    }
                    var values = parser.ReadFields();
                    var csvData = new ExcelDataModel
                    {
                        Id = Convert.ToInt64(values[0]),
                        LeadDate = Convert.ToDateTime(values[1] == "NULL" ? DateTime.MinValue : values[1]),
                        LeadSourceId = Convert.ToInt64(values[2] == "NULL" ? 0 : values[2]),
                        SourceNameId = Convert.ToInt64(values[3] == "NULL" ? 0 : values[3]),
                        UserName = values[4],                        // Added
                        LeadStatusId = Convert.ToInt64(values[5] == "NULL" ? 0 : values[5]),
                        MobileNo = values[6],                        // Added
                        Email = values[7],                           // Added
                        Note = values[8],                            // Added
                        LeadOwnerId = Convert.ToInt64(values[9] == "NULL" ? 0 : values[9]),
                        StatusId = Convert.ToInt64(values[10] == "NULL" ? 0 : values[10]),      // Added
                        CreatedBy = Convert.ToInt64(values[11] == "NULL" ? 0 : values[11]),     // Added
                        CreatedDate = Convert.ToDateTime(values[12] == "NULL" ? DateTime.MinValue : values[12]), // Added
                        ModifiedBy = Convert.ToInt64(values[13] == "NULL" ? 0 : values[13]),    // Added
                        ModifiedDate = Convert.ToDateTime(values[14] == "NULL" ? DateTime.MinValue : values[14]),// Added
                        IsDeleted = false,    // Added   values[] == "NULL" ? 0 : values[]
                        SourceTypeId = Convert.ToInt64(values[16] == "NULL" ? 0 : values[16]),  // Added
                        UserTypeId = Convert.ToInt64(values[17] == "NULL" ? 0 : values[17])     // Added
                    };
                    //dbContext.CsvData.Add(csvData);
                }
                return 1;
            }
            else
            {
                fileStream.Close();
                return -1;
            }

            return 0;
        }
