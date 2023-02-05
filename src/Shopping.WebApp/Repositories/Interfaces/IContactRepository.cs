using Shopping.WebApp.Entities;

namespace Shopping.WebApp.Repositories;

public interface IContactRepository
{
    Task<Contact> SendMessage(Contact contact);
    Task<Contact> Subscribe(string address);
}
