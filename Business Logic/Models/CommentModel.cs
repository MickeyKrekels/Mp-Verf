using Core.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Models
{
    public class CommentModel : EntityModalBase
    {
        public string Text { get; set; }
        public DateTime DataCreated { get; set; }
        public decimal RatingAverage
        {
            get
            {
                if (commentRatings == null || commentRatings.Count() == 0)
                {
                    return 0;
                }
                var average = (decimal)commentRatings.Sum(x => x.Rating) / (decimal)commentRatings.Count();
                var starRating = average / 2;
                return Decimal.Round(starRating, 2);
            }
        }
        public Guid OwnerId { get; set; }
        public Guid StoreItemId { get; set; }
        public List<CommentRatingModel> commentRatings { get; set; }
    }
}
