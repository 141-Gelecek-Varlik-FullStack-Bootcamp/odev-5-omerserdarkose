using HelenSposa.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.Entities.Dtos.Claim
{
    public class ClaimShowDto:IDto
    {
        public int Id { get; set; }

        public string ClaimName { get; set; }
    }
}
