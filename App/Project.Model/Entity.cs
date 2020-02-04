using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model
{
    public class Entity
    {
        [Key]
        [Index("IX_Id", IsUnique = true)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Modified { get; set; }

        public string ModifiedBy { get; set; }
    }
}
