using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PaginacionDepartamentos.Models
{
    [Table("EMP")]
    public class Empleado
    {
        [Key]
        [Column("EMP_NO")]
        public int EMP_NO { get; set; } // Cambiado a EMP_NO
        [Column("APELLIDO")]
        public string APELLIDO { get; set; } // Cambiado a APELLIDO
        [Column("OFICIO")]
        public string OFICIO { get; set; } // Cambiado a OFICIO
        [Column("SALARIO")]
        public int SALARIO { get; set; } // Cambiado a SALARIO
        [Column("DEPT_NO")]
        public int DEPT_NO { get; set; } // Cambiado a DEPT_NO
    }
}
