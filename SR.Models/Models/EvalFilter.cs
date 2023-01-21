using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SR.Models.Models
{
    public  class EvalFilter
    {
        [DefaultValue("00000000-0000-0000-0000-000000000000")]
        public Guid Id { get; set; }
        [Range(0, 5,ErrorMessage = "Enter a value between 0 and 5")]
        [DefaultValue(0)]
        public int Stars { get; set; }
    }
}
