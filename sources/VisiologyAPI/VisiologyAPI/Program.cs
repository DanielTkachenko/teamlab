using System.Text.Json;
using System.Text.Json.Serialization;
using ViQube.Model;
using ViQube.Model.Query;
using ViQube.Provider;


namespace VisiologyAPI
{

    class Program
    {
        private static async Task Main(string[] args)
        {
            var token = await new AccessToken().GetTokenDictionary("admin","123456");

            var dbName = "incom";
            var tableName = "TestRecords";
            var data = new DatabasesMethods();

            Console.WriteLine(token["access_token"]);

            Console.WriteLine();

            var test = new MetaDataQuery()
            {
                Database = "incom",
                Groups = new List<Group>()
                {
                    new Group()
                    {
                        Mgid = "Posti",
                        Measures = new List<Measure>()
                        {
                            new Measure()
                            {
                                Mid = "Kolichestvo_postov",

                            }
                        }
                    }
                },
                Limit = new Limit()
                {
                    Columns = 3,
                    Rows = 3
                }
            };

            var check = new QueryMethods();
            await check.CreateMetaDataQueryAsync(test);

            //var data = new DatabasesMethods();
            //var listRecord = new Record();
            //var list =await data.GetRecordsAsync(dbName, "myorion");

            //foreach (var value in list.Values)
            //{
            //    for (var i = 0; i < list.Values.Count; i++)
            //    {
            //        var check = value[i];
            //    }
            //}
            //if(list.Values.Contains(listRecord.Values[0]))
            //{

            //}



            //await SetRecords(client, dbName, tableName, data);
            //var records = await data.GetRecordsAsync(client, dbName, tableName);

            //foreach (var column in records.columns)
            //{
            //    Console.WriteLine(column);
            //}
            //foreach (var values in records.values.SelectMany(value => value))
            //{
            //    Console.WriteLine(values);
            //}

        }

        public static async Task SetRecords(HttpClient client,string dbName, string tableName, DatabasesMethods data)
        {
            Record? record = null;

            //Десериализация через файл
            //JsonSerializer serializer = new JsonSerializer();
            //using (StreamReader streamReader = new StreamReader(@"C:\GitHub\123.json"))
            //using (JsonReader reader = new JsonTextReader(streamReader))
            //{
            //    record = serializer.Deserialize<Record>(reader);
            //}

            var valuesList = new List<List<object>>(record.Values);

            var columnsList = await data.GetColumnsAsync( dbName, tableName);
            var test = columnsList.Select(columns => columns.NameTable).ToList();

            var records = new Record()
            {
                Columns = test,
                Values = valuesList
            };
            await data.PostRecordsAsync(dbName, tableName, records);
        }

        public static List<Column> MethodColumns(string[] input)
        {
            var list = new List<string>(input);
            var columnList = new List<string>();
            var typeList = new List<string>();

            foreach (var type in list)
            {
                var test = type.Split(" ");
                columnList.Add(test[0]);
                typeList.Add(test[1]);
            }

            var columns = new List<Column>();
            for (int i = 0; i < columnList.Count; i++)
            {
                var data=new DatabasesMethods();
                columns.Add(data.CreateColumns(columnList[i], typeList[i]));
            }

            return columns;
        }

        public static async Task CheckingDeleteMethodsMetadata()
        {
            var data = new MetaDataMethods();
            var testBase = "MetaTest";
            var dbName = "incom";


            await data.DeleteMeasureAsync(testBase, "change", "Kupleno");
            await data.DeleteDimensionRoleAsync(testBase, "change", "2");
            await data.DeleteMeasureGroupAsync(testBase, "change");
            await data.DeleteAllDimensionRolesAsync(testBase, "change");
            await data.DeleteAllMeasuresAsync(testBase, "change");
            await data.DeleteAllMeasureGroupsAsync(testBase);


            await data.DeleteDimensionAttributeAsync(testBase, "1", "");
            await data.DeleteDimensionAsync(testBase, "1");
            var jsonstring = @"{
                ""data"":  {
                ""column"": ""Продукт"",
                ""table"": ""myorion""
                },
                ""meta"": {
                ""attrid"": ""Produkt"",
                ""dimid"": ""Change""
                },
                ""type"": ""DIM""
            }";
            var check = JsonSerializer.Deserialize<BindingDim>(jsonstring);
            await data.DeleteBindingAsync(testBase, check);
            await data.DeleteAllDimensionsDatabaseAsync(testBase);
            await data.DeleteAllBindingsAsync(testBase);
            await data.DeleteMetaDatabaseAsync(testBase);
        }

        public static async Task CheckingUpdateMethodsMetadata()
        {
            var data = new MetaDataMethods();
            var testBase = "MetaDataTest";
            var dbName = "incom";

            var attr = new DimensionAttributes()
            {
                Id = "Produkt",
                Name = "Produkt"
            };
            await data.UpdateDimensionAttributesAsync(testBase, "Produkt", "Change", attr);

            var dim = new DimensionsCreate()
            {
                Name = "Opion"
            };
            await data.UpdateDimensionAsync(testBase, "Change", dim);
            var dimRoles = new DimensionRolesGet()
            {
                Id = "2",
                IsPrimary = true,
                Name = "Check"
            };
            await data.UpdateDimensionRoleAsync(testBase, "test", "5", dimRoles);

            await data.UpdateMeasureAsync(testBase, "test", "Kolichestvo_postov",
            await data.GetMeasureAsync(dbName, "Produkt", "Kupleno"));
            var measureGroups = new MeasureGroups()
            {
                Id = "change",
                Name = "change"
            };

            await data.UpdateMeasureGroupAsync(testBase, "test", measureGroups);
            var metaData = new MetaDatabase()
            {
                Name = "MetaTest",
                DatabaseName = "MetaTest",
                Id = "MetaTest"
            };
            await data.UpdateMetaDatabaseAsync(testBase, metaData);
        }

        public static async Task CheckingPostMethodsMetadata()
        {
            var data = new MetaDataMethods();
            var testBase = "MetaDataTest";
            var dbName = "incom";
            await new DatabasesMethods().PostCreateDatabaseAsync(testBase);

            var measureGroup = new MeasureGroups()
            {
                Id = "test",
                Name = "test",
                TableName = "testTable"
            };
            await data.CreateMeasureGroupAsync(testBase, measureGroup);
            await data.CreateMeasuresAsync(testBase, "test", await data.GetMeasureAsync(dbName, "Posti", "Kolichestvo_postov"));
            await data.CreateDimensionsDatabaseAsync(testBase, new DimensionsCreate() { Id = "1", Name = "test" });
            var dimRoles = new DimensionRolesGet()
            {
                Id = "5",
                DimId = "1",
                IsPrimary = true,
                Name = "Simulator"
            };
            await data.CreateDimensionRoleAsync(testBase, "test", dimRoles);
            await data.CreateDimensionAttributesAsync(testBase, "Produkt", await data.GetAttributeAsync(dbName, "Produkt", "Produkt"));
            var binding = await data.GetBindingsAsync(dbName);
            var test = binding.Find(dim => dim.Meta.AttrId == "Produkt");
            await data.CreateBindingsAsync(testBase, test);
        }

        public static async Task CheckingGetMethodsMetadata(string dbName)
        {
            var data = new MetaDataMethods();
            Console.WriteLine(await data.GetAllMetaDataBasesAsync());
            Console.WriteLine(await data.GetAllDimensionsDatabaseAsync(dbName));
            Console.WriteLine(await data.GetAllMeasureGroupsAsync(dbName));
            Console.WriteLine(await data.GetAllDimensionAttributesAsync(dbName, "Produkt"));
            Console.WriteLine(await data.GetAllDimensionsRolesAsync(dbName, "Posti"));
            Console.WriteLine(await data.GetAllMeasuresAsync(dbName, "Posti"));
            Console.WriteLine(await data.GetMetaDatabaseAsync(dbName));
            Console.WriteLine(await data.GetBindingsAsync(dbName));
            Console.WriteLine(await data.GetMeasureGroupAsync(dbName, "Posti"));
            Console.WriteLine(await data.GetDimensionAsync(dbName, "Produkt"));
            Console.WriteLine(await data.GetDimensionRoleAsync(dbName, "Posti", "654"));
            Console.WriteLine(await data.GetMeasureAsync(dbName, "Posti", "Kolichestvo_postov"));
            Console.WriteLine(await data.GetAttributeAsync(dbName, "Produkt", "Produkt"));
            Console.WriteLine(await data.GetAttributeValuesAsync(dbName, "Produkt", "Produkt"));
        }

        public static async void CheckingQuery()
        {
            var order = new List<OrderBy>()
            {
                new OrderBy()
                {
                    Column = "Принято",
                    Function = "MAX",
                    Order = "ASC"
                }
            };
            var query = new QuerySelect()
            {
                From = "myorion",
                GroupBy = new List<string>()
                {
                    "Магазин",
                    "Продукт",
                    "Принято",
                    "Просмотренно",
                    "Выбрано",
                    "Куплено"
                },
                OrderBy = order,
                Limit = 10,
                Offset = 0

            };
            var test = new QueryMethods();
            await test.CreateDatabaseQueryAsync("incom", query);
        }
    }
}