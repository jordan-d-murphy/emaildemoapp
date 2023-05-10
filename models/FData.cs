using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models;

public class FData
{
    public int Id { get; set; }


    public string? Email { get; set; }
    public string? Password { get; set; }


    public string? Name { get; set; }



    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Zip { get; set; }



    public string? TextArea { get; set; }



    public bool Check1 { get; set; }
    public bool Check2 { get; set; }
    public bool Check3 { get; set; }



    [DataType(DataType.DateTime)]
    public DateTime SubmittedDate { get; set; }
   
   

}