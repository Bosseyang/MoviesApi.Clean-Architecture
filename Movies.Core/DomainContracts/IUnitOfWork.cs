using Movies.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.DomainContracts;

public interface IUnitOfWork
{ 
    IMovieRepository Movies { get; }
    IReviewRepository Reviews { get; }
    IActorRepository Actors { get; }
    Task CompleteAsync();
}
