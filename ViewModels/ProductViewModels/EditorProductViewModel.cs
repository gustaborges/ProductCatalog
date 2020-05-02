using Flunt.Notifications;
using Flunt.Validations;

namespace ProductCatalog.ViewModels.ProductViewModels
{
    // Edit => serve tanto para Update quanto para Create
    public class EditorProductViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsGreaterOrEqualsThan(Id, 0, "Description", "Id inválido" )
                .IsNotNull(Description, "Description", "A descrição deve ser fornecida" )
                .HasMaxLen(Title, 120, "Title", "O título deve conter até 120 caracteres")
                .HasMinLen(Title, 3, "Title", "O título deve conter pelo menos 3 caracteres")
                .IsGreaterThan(Price, 0, "Price", "O preço deve ser maior que zero")
                .IsGreaterThan(Quantity, 0, "Quantity", "A quantidade deve ser maior que zero")                
            );
        }
    }
}