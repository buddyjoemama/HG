using HumanGavel.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Data.Entities
{
    public partial class Case
    {
        /// <summary>
        /// Gets all enabled, unmarked cases ordered by expiration date.
        /// </summary>
        public static List<CaseResult> GetByCategory(Category cat, int skip = 0, int take = 25)
        {
            if (cat == Category.Trending)
                return GetTrendingCases(skip, take);

            return null;
        }

        /// <summary>
        /// Get all enabled/!markfordelete cases.
        /// </summary>
        /// <returns></returns>
        public static List<Case> GetAllCases()
        {
            using (HGDataContext context = new Data.HGDataContext())
            {
                return context
                    .Cases
                    .Include("Participants")
                    .Where(s => !s.MarkForDelete && s.IsEnabled)
                    .OrderBy(s => s.ExpirationDate)
                    .ToList();
            }
        }

        /// <summary>
        /// Trending cases are all those most recently voted on..with the added ability to page results.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static List<CaseResult> GetTrendingCases(int skip = 0, int top = 100)
        {
            using (HGDataContext context = new Data.HGDataContext())
            {
                var skipParam = new SqlParameter("@skip", skip);
                var takeParam = new SqlParameter("@take", top);

                return context
                    .Database
                    .SqlQuery<CaseResult>("exec GetTrending @skip, @take", skipParam, takeParam)
                    .ToList();
            }
        }

        public static List<Case> GetPopularCases(int skip = 0, int top = 100)
        {
            using (HGDataContext context = new Data.HGDataContext())
            {
                return context
                    .Votes
                    .Include("Case")
                    .Include("Case.Participants")
                    .GroupBy(s => s.Case)
                    .OrderByDescending(s => s.Sum(t => t.Value))
                    .Select(s => s.Key)
                    .Skip(skip)
                    .Take(top)
                    .Distinct()
                    .ToList();
            }
        }

        /// <summary>
        /// Returns a list of featured cases. Need a bg job to prune these.
        /// </summary>
        /// <returns></returns>
        public static List<Case> GetFeaturedCases()
        {
            using (HGDataContext context = new Data.HGDataContext())
            {
                return context.Cases.Where(s => s.IsFeatured && s.IsEnabled && !s.MarkForDelete).ToList();
            }
        }
    }
}
