using Flunt.Notifications;
using Flunt.Validations;

namespace ProductCatalog.ViewModels.CategoryViewModels
{
    public class EditorCategoryViewModel : Notifiable, IValidatable
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterOrEqualsThan(Id, 0, nameof(Id), "Id inválido")
                .IsNotNullOrEmpty(Title, nameof(Title), "Title deve ser fornecido")
                .HasMinLen(Title, 3, nameof(Title), "Title deve possuir no mínimo 3 caracteres")
                .HasMaxLen(Title, 120, nameof(Title), $"Title deve possuir no máximo 120 caracteres")
            );
        }
    }
}