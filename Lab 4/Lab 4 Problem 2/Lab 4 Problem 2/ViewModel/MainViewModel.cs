using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Lab_4_Problem_2.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Lab_4_Problem_2.View;
using System.Linq;
//using AddWindow = Lab_4_Problem_2.Views.AddWindow;

namespace Lab_4_Problem_2.ViewModel
{
    /// <summary>
    /// The VM for the main screen that shows the member list.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// The list of registered members.
        /// </summary>
        private ObservableCollection<Member> members;

        /// <summary>
        /// The currently selected member.
        /// </summary>
        private Member selectedMember;

        /// <summary>
        /// The database that keeps track of saving and reading the registered members.
        /// </summary>
        private MemberDB database;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            members = new ObservableCollection<Member>();
            database = new MemberDB(members);
            members = database.GetMemberships();
            AddCommand = new RelayCommand(AddMethod);
            ExitCommand = new RelayCommand<IClosable>(ExitMethod);
            ChangeCommand = new RelayCommand(ChangeMethod);
            Messenger.Default.Register<MessageMember>(this, ReceiveMember);
            Messenger.Default.Register<NotificationMessage>(this, ReceiveMessage);
        }

        /// <summary>
        /// The command that triggers adding a new member.
        /// </summary>
        public ICommand AddCommand { get; private set; }
        /// <summary>
        /// The command that triggers closing the application
        /// </summary>
        public ICommand ExitCommand { get; private set; }
        /// <summary>
        /// The command that triggers change a new member.
        /// </summary>
        public ICommand ChangeCommand { get; private set; }

        /// <summary>
        /// The currently selected member in the list box.
        /// </summary>
        public Member SelectedMember
        {
            get
            {
                return selectedMember;
            }
            set
            {
                selectedMember = value;
                RaisePropertyChanged("SelectedMember");
            }
        }

        /// <summary>
        /// Shows a new add screen.
        /// </summary>
        public void AddMethod()
        {
            AddWindow add = new AddWindow();
            add.Show();
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        /// <param name="window">The window to close.</param>
        public void ExitMethod(IClosable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        /// <summary>
        /// Opens the change window.
        /// </summary>
        public void ChangeMethod()
        {
            if (SelectedMember != null)
            {
                ChangeWindow change = new ChangeWindow();
                change.Show();
                Messenger.Default.Send(SelectedMember);
            }
        }

        /// <summary>
        /// Gets a new member for the list.
        /// </summary>
        /// <param name="m">The member to add. The message denotes how it is added.
        /// "Update" replaces at the specified index, "Add" adds it to the list.</param>
        public void ReceiveMember(MessageMember m)
        {
            if (m.Message == "Update")
            {
                var index = MemberList.IndexOf(SelectedMember);

                if (index != -1)
                    MemberList[index] = m;
                selectedMember = m;
                this.RaisePropertyChanged(() => this.MemberList);
                database.SaveMemberships();
            }
            else if (m.Message == "Add")
            {
                members.Add(m);
                this.RaisePropertyChanged(() => this.MemberList);
                database.SaveMemberships();
            }
        }


        /// <summary>
        /// Gets text messages.
        /// </summary>
        /// <param name="msg">The received message. "Delete" means the currently selected member is deleted.</param>
        public void ReceiveMessage(NotificationMessage msg)
        {
            if (msg.Notification == "Delete")
            {
                members.Remove(selectedMember);
                //members.Remove(members.Where(i => i.FirstName == selectedMember.FirstName).Single());
                //members.Remove((Member)this.selectedMember);
                this.RaisePropertyChanged(() => this.MemberList);
                database.SaveMemberships();
            }
        }

        /// <summary>
        /// The list of registered members.
        /// </summary>
        public ObservableCollection<Member> MemberList
        {
            get { return members; }
        }
        
    }
}
