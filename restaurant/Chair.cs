using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace restaurant
{
    /// <summary>
    /// Represents a chair in the restaurant, including its status and position.
    /// </summary>
    public class Chair
    {
        /// <summary>
        /// Enum representing the status of a chair.
        /// </summary>
        public enum ChairStatus : int
        {
            EMPTY = 0,
            OCCUPIED = 1,
            DIRTY = 2
        }

        /// <summary>
        /// The current status of the chair (e.g., EMPTY, OCCUPIED, DIRTY).
        /// </summary>
        public ChairStatus Status { get; set; }

        /// <summary>
        /// Position X on Board
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Position Y on Board
        /// </summary>
        public int Y { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Chair"/> class based on a given position.
        /// </summary>
        /// <param name="startPosition">The initial position and status to assign to the chair.</param>
        /// <param name="status">The initial status to assign to the chair.</param>
        public Chair(int X, int Y, ChairStatus Status)
        {
            this.X = X;
            this.Y = Y;
            this.Status = Status;
        }
    }
}