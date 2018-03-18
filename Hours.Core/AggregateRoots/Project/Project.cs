using System;
using System.Collections.Generic;
using System.Text;

namespace Hours.Core.AggregateRoots.Project
{
    /// <summary>
    /// 
    /// </summary>
    public class Project : AggregateRoot
    {
        public Project(
            string name,
            int businessUnitId,
            string description,
            int version = 0)
        {
        }

        public string Name { get; private set; }       

        public string Description { get; private set; }

        public int BusinessUnitId { get; private set; } 
    }
}
