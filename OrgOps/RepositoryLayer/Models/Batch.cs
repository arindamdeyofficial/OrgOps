using System;
using System.Collections.Generic;

namespace RepositoryLayer.Models
{
    public partial class Batch
    {
        public string BatchId { get; set; }
        public short? BatchStudentNumber { get; set; }
        public string BatchTeacherId { get; set; }
    }
}
