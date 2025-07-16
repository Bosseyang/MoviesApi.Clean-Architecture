using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.DomainContracts;

public interface IReviewRepository
{
    Task<bool> MovieExistsAsync(int movieId);
    Task<IEnumerable<Review>> GetReviewsByMovieAsync(int movieId);

    void Add(Review review);
    //void Update(Review review);
    void Remove(Review review);
}
