using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JsonReader
{
    #region A set of data classes that represent the structure of the standard JSON file.
    public class Batter
    {
        public string id { get; set; }
        public string type { get; set; }
    }

    public class Batters
    {
        public List<Batter> batter { get; set; }
    }

    public class Topping
    {
        public string id { get; set; }
        public string type { get; set; }
    }

    public class Item
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public double ppu { get; set; }
        public Batters batters { get; set; }
        public List<Topping> topping { get; set; }
    }

    public class Items
    {
        public List<Item> item { get; set; }
    }

    public class Root
    {
        public Items items { get; set; }
    }
    #endregion


    /// <summary>
    /// A class that is responsible to convert the passed in JSON data into the respective Root object. After the JSON data is deserialized, it can then generate a
    /// HTML table.
    /// </summary>
    public class JsonToTable
    {
        /// <summary>
        /// A class variable that holds the passed in JSON data.
        /// </summary>
        private readonly string _json = "";

        /// <summary>
        /// A default constructor that accepts JSON data.
        /// </summary>
        /// <param name="json">string in JSON format</param>
        public JsonToTable(string json)
        {
            _json = json;
        }

        /// <summary>
        /// A function that serialize the JSON data, generates a HTML page, and generates a HTML table.
        /// </summary>
        /// <returns>A valid HTML page in string.</returns>
        public string ConvertToTable()
        {            
            Root obj = JsonSerializer.Deserialize<Root>(_json);
            var page = GeneratePageStructure();
            var table = GenerateTable(obj);

            page = page.Replace("@@Table", table);

            return page;
        }

        /// <summary>
        /// A function that generates the HTML page strcuture.
        /// @@Table placeholder is inserted for the actual HTML table replacement.
        /// </summary>
        /// <returns>Valid HTML string that represents the page.</returns>
        private string GeneratePageStructure()
        {
            StringBuilder result = new StringBuilder();
            result.Append("<!DOCTYPE html>");
            result.Append("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            result.Append("<head>");
            result.Append("<meta charset=\"utf-8\" />");
            result.Append("<title>Json Table</title>");
            result.Append("</head>");
            result.Append("<body>");
            result.Append("@@Table");
            result.Append("</body>");
            result.Append("</html>");
            return result.ToString();
        }


        /// <summary>
        /// A function that generates HTML table based on the Root data object.
        /// </summary>
        /// <param name="data">The object representation of the JSON string.</param>
        /// <returns>A HTML table.</returns>
        private string GenerateTable(Root data)
        {
            StringBuilder result = new StringBuilder();
            result.Append("<table style=\"width:100%;text-align:left\" border = \"1\">");
            result.Append("<tr>");
            result.Append("<th>Id</th>");
            result.Append("<th>Type</th>");
            result.Append("<th>Name</th>");
            result.Append("<th>Batter</th>");
            result.Append("<th>Topping</th>");
            result.Append("</tr>");            
            

            foreach(var item in data.items.item)
            {
                foreach(var batter in item.batters.batter)
                {
                    foreach (var topping in item.topping) //in order to have topping display for each batter.
                    {
                        string row = GenerateRow(item.id, item.type, item.name, batter.type, topping.type);
                        result.Append(row);
                    }
                }
            }

            result.Append("</table>");
            return result.ToString();
        }

        /// <summary>
        /// A function that generates the HTML table row <tr></tr>
        /// </summary>
        /// <param name="id">item id</param>
        /// <param name="type">item type</param>
        /// <param name="name">item name</param>
        /// <param name="batter">item batter(s)</param>
        /// <param name="topping">item topping(s)</param>
        /// <returns>A valid HTML table row</returns>
        private string GenerateRow(string id, string type, string name, string batter, string topping)
        {
            StringBuilder result = new StringBuilder();

            result.Append("<tr>");
            result.Append($"<td>{id}</td>");
            result.Append($"<td>{type}</td>");
            result.Append($"<td>{name}</td>");
            result.Append($"<td>{batter}</td>");
            result.Append($"<td>{topping}</td>");
            result.Append("</tr>");

            return result.ToString();
        }
    }
}
