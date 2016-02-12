using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warden.DataModel.Entities
{
    public abstract class EntityBase
    {
        /// <summary>
        /// The unqiue WaypointId of the entity object
        /// </summary>
        public Guid Id { get; set; }
    }
}
