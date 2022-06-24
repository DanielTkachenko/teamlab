using System.Collections;
using System.Reflection;
using TeamlabLogger.Services;
using ViQube.Model;

namespace Downloader
{
    public class ParserVkToViqube
    {
        /// <summary>
        /// Преобразует данные полученные из VK в данные необходимые для записи в ViQube
        /// </summary>
        /// <param name="record">Данные полученные из VK которые хотим записать в ViQube</param>
        /// <returns>Возвращает преобразованные данные</returns>
        public static Record TryParseToViQube(object record)
        {
            try
            {
                var columnName = new List<string>();
                var listValues = new List<List<object?>>();

                if (record is IEnumerable list)
                {
                    foreach (var record2 in list)
                    {
                        var values = new List<object?>();
                        var propertyInfo = record2.GetType().GetProperties();
                        foreach (var property in propertyInfo)
                        {
                            if (columnName.Count < propertyInfo.Length)
                            {
                                columnName.Add(property.Name.ToString());
                            }
                            values.Add(property.GetValue(record2));
                        }
                        listValues.Add(values);
                    }
                }
                var visiologyRecord = new Record()
                {
                    Values = listValues,
                    Columns = columnName
                };
                return visiologyRecord;
            }
            catch (Exception ex)
            {
                TeamlabLogger.Services.LoggerService<ParserVkToViqube> logger = new LoggerService<ParserVkToViqube>();
                logger.Error(ex.Message);
                throw ex;
            }
        }
    }
}