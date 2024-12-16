using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Comentario
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo Autor é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome do autor pode ter no máximo 100 caracteres.")]
    public string Autor { get; set; }

    [Required(ErrorMessage = "O campo Texto é obrigatório.")]
    [StringLength(500, ErrorMessage = "O texto pode ter no máximo 500 caracteres.")]
    public string Texto { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Data de Criação")]
    public DateTime DataCriacao { get; set; }

    public int? ComentarioPaiId { get; set; } 
    public virtual Comentario ComentarioPai { get; set; }
    public virtual ICollection<Comentario> Respostas { get; set; }
}
