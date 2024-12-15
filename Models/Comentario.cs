using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Comentario
{
    public int Id { get; set; }

    [Required]
    [MaxLength(500)]
    public string Texto { get; set; }

    [Required]
    [MaxLength(100)]
    public string Autor { get; set; }

    public DateTime DataCriacao { get; set; }

    public int? ComentarioPaiId { get; set; }

    public virtual Comentario ComentarioPai { get; set; }
    public virtual ICollection<Comentario> Respostas { get; set; }
}
