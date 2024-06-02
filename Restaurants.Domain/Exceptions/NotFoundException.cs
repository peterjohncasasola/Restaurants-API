namespace Restaurants.Domain.Exceptions;

public class NotFoundException(string resourceType, string resourceKey) 
: Exception($"{resourceType} with Id: {resourceKey} doesn't exists")
{

}
