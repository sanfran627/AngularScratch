using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularScratch
{
  public class User
  {
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DOB { get; set; }
    public string Title { get; set; }
  }
}
