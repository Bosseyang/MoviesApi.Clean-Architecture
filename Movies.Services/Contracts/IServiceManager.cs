using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Services.Contracts;

public interface IServiceManager
{
    IMovieService Movies { get; }
    IReviewService Reviews { get; }
    IActorService Actors { get; }
}
