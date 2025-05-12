using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;


namespace restaurant
{
    /// <summary>
    /// Data Transfer Object representing a chair with position and status.
    /// </summary>
    public class ChairData
    {
        /// <summary>
        /// The X coordinate of the chair.
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// The Y coordinate of the chair.
        /// </summary>
        public int y { get; set; }

        /// <summary>
        /// The status of the chair (e.g., "EMPTY", "OCCUPIED").
        /// </summary>
        public string status { get; set; }
    }

    /// <summary>
    /// Data Transfer Object representing a table with position, status, and a list of chairs.
    /// </summary>
    public class TableData
    {
        /// <summary>
        /// The X coordinate of the table.
        /// </summary>
        public int x { get; set; }

        /// <summary>
        /// The Y coordinate of the table.
        /// </summary>
        public int y { get; set; }

        /// <summary>
        /// The numeric status of the table.
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// The list of chairs assigned to the table.
        /// </summary>
        public List<ChairData> chairs { get; set; }
    }
    public class CustomerDataTS
    {
        public int xStart { get; set; }
        public int yStart { get; set; }
        public int xEnd { get; set; }
        public int yEnd { get; set; }

    }
    /// <summary>
    /// The root layout object containing a list of tables.
    /// </summary>
    public class RootData
    {
        const int XMAX = 40;
        const int YMAX = 40;
        /// <summary>
        /// The list of tables in the layout.
        /// </summary>
        public List<TableData> tables { get; set; }

        public int[,] room = new int[XMAX, YMAX];
        public List<CustomerDataTS> customerDataTS { get; set; }

    }

    /// <summary>
    /// Contains general enums and utility methods for restaurant layout management.
    /// </summary>
    public class General
    {
          /// <summary>
        /// Deserializes a JSON file into a <see cref="LayoutRoot"/> object.
        /// </summary>
        /// <param name="file">The path to the JSON file.</param>
        /// <returns>A <see cref="LayoutRoot"/> object containing the layout data.</returns>
        public RootData DeseriliazeInit(string file)
        {
            string json = File.ReadAllText(file);
            RootData layout = JsonConvert.DeserializeObject<RootData>(json);
            return layout;
        }

        /// <summary>
        /// Serializes a <see cref="LayoutRoot"/> object and writes it to a JSON file.
        /// </summary>
        /// <param name="layout">The layout object to serialize.</param>
        /// <param name="file">The path to the output JSON file.</param>
        public void SerializeLayout(RootData layout, string file)
        {
            string json = JsonConvert.SerializeObject(layout, Formatting.Indented);
            File.WriteAllText(file, json);
        }

        /// <summary>
        /// Converts layout data from DTOs to actual <see cref="Table"/> objects with position and chair status.
        /// </summary>
        /// <param name="data">The layout root containing a list of table DTOs with their chair data.</param>
        /// <returns>A list of <see cref="Table"/> objects created from the given layout data.</returns>
        public List<Table> CreateTables(RootData data)
        {
            // Temporary list to hold the converted Table objects
            List<Table> tempTableData = new List<Table>();

            // Iterate through each TableDTO in the layout
            foreach (TableData table in data.tables)
            {
                // List to hold the chair positions for the current table
                List<ChairData> chairPos = new List<ChairData>();
                Enum.TryParse<Table.TableStatus>(table.status, out var status);
                Table.TableStatus tableStatus = status;

                // Iterate through each ChairDTO and convert to Pos with proper status
                foreach (ChairData chair in table.chairs)
                {
                    //Enum.TryParse<Chair.ChairStatus>(chair.status, out var chairStatus);
                    ChairData chairData = new ChairData() ;
                    chairData.x = chair.x;
                    chairData.y = chair.y;
                    chairData.status = chair.status;

                    chairPos.Add(chairData);
                }

                // Create a new Table object and add it to the result list
                tempTableData.Add(new Table(table.x,table.y, status, chairPos));
            }

            // Return the list of constructed Table objects
            return tempTableData;
        }

    }
}
