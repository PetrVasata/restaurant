using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace restaurant
{
    /// <summary>
    /// Represents a table in the restaurant, including its position and the chairs around it.
    /// </summary>
    public class Table
    {

        /// <summary>
        /// Enum representing the status of a Table.
        /// </summary>
        public enum TableStatus : int
        {
            EMPTY = 0,
            OCCUPIED = 1,
            DIRTY = 2
        }

        /// <summary>
        /// The list of chairs associated with this table.
        /// </summary>
        public List<Chair> chairs { get; set; }

        /// <summary>
        /// Position X on Board
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Position Y on Board
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Tabel status
        /// </summary>
        public TableStatus Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Table"/> class with a table position and a list of chair positions.
        /// </summary>
        /// <param name="table">The position of the table.</param>
        /// <param name="positions">A list of positions for each chair.</param>
        public Table(int X, int Y, TableStatus TableStatus,List<ChairData> charsData)
        {
            chairs = new List<Chair>();
            this.X = X;
            this.Y = Y;
            this.Status = TableStatus;

            foreach (ChairData chairData in charsData)
            {
                Enum.TryParse<Chair.ChairStatus>(chairData.status, out var status);
                chairs.Add(new Chair(chairData.x, chairData.y, status));
            }
        }
    }
}
