using System;
using DependencyInjection.Models;

namespace DependencyInjection.Infrastructure
{
    public static class TypeBroker
    {
        private static Type repoType = typeof(MemoryRepository);

        private static IRepository testRepo;

        public static IRepository Repository => 
            testRepo ?? Activator.CreateInstance(repoType) as IRepository;

        public static void SetRepositoryType<T>() where T : IRepository => repoType = typeof(T);

        public static void SetTestObjet(IRepository repo){
            testRepo = repo;
        }
    }
}