using Flunt.Notifications;
using Flunt.Validations;

namespace MinimalApi.ViewModels
{
    public class UpdateTodoViewModel : Notifiable<Notification>
    {
        public string Title { get; set; }
        public Guid TodoId { get; set; }

        public void MapTo()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(Title, "Informe o novo título da tarefa")
                .IsNotNull(TodoId, "Informe o Id título da tarefa")
                .IsGreaterThan(Title, 5, "O novo título da tafera deve conter mais de 5 caracteres"));
        }
    }
}
