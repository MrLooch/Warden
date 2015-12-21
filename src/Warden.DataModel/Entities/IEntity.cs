using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warden.DataModel.Entities
{
    public interface IEntity
    {
        /// <summary>
        /// The unqiue WaypointId of the entity object
        /// </summary>
        int Id { get; set; }
    }
}
