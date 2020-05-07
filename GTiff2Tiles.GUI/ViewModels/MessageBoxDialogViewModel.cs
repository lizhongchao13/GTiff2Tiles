﻿using System.Windows;
using Prism.Mvvm;
using Prism.Commands;
using GTiff2Tiles.GUI.Localization;
using MaterialDesignThemes.Wpf;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

namespace GTiff2Tiles.GUI.ViewModels
{
    /// <inheritdoc />
    /// <summary>
    /// Custom message box dialog. <see cref="T:GTiff2Tiles.GUI.Views.MessageBoxDialogView" />.
    /// </summary>
    // ReSharper disable once MemberCanBeInternal
    public sealed class MessageBoxDialogViewModel : BindableBase//: PropertyChangedBase
    {
        #region Properties and fields

        #region UI

        /// <summary>
        /// Copy to clipboard's button's hint's text.
        /// </summary>
        public string CopyToClipboardToolTip { get; } = Strings.CopyToClipboardToolTip;

        /// <summary>
        /// Accept button's text.
        /// </summary>
        public string AcceptButtonContent { get; } = Strings.AcceptButtonContent;

        /// <summary>
        /// Cancel button's text.
        /// </summary>
        public string CancelButtonContent { get; } = Strings.CancelButtonContent;

        /// <summary>
        /// Dialog's width.
        /// </summary>
        public int Width { get; } = Enums.Dialogs.Width;

        /// <summary>
        /// Dialog's height.
        /// </summary>
        public int Height { get; } = Enums.Dialogs.Height;

        #endregion

        #region Backing fields

        private string _message;

        private Visibility _cancelButtonVisibility = Visibility.Collapsed;

        #endregion

        /// <summary>
        /// Text inside MessageBox.
        /// </summary>
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        /// <summary>
        /// Controls visibility of Cancel button on MessageBox.
        /// </summary>
        public Visibility CancelButtonVisibility
        {
            get => _cancelButtonVisibility;
            set => SetProperty(ref _cancelButtonVisibility, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create message box.
        /// </summary>
        /// <param name="message">Text, that you want to see on message box.</param>
        /// <param name="isCancelButtonVisible">Set to <see langword="true"/>, if you want to see Cancel button.</param>
        public MessageBoxDialogViewModel(string message, bool isCancelButtonVisible = false)
        {
            Message = message;
            CancelButtonVisibility = isCancelButtonVisible ? Visibility.Visible : Visibility.Collapsed;

            //Bind delegates with methods
            CopyToClipboardButtonCommand = new DelegateCommand(CopyToClipboardButton);
            AcceptButtonCommand = new DelegateCommand(AcceptButton);
            CancelButtonCommand = new DelegateCommand(CancelButton);
        }

        #endregion

        #region DelegateCommands

        /// <summary>
        /// CancelButton DelegateCommand
        /// </summary>
        public DelegateCommand CancelButtonCommand { get; }

        /// <summary>
        /// AcceptButton DelegateCommand
        /// </summary>
        public DelegateCommand AcceptButtonCommand { get; }

        /// <summary>
        /// CopyToClipboardButton DelegateCommand
        /// </summary>
        public DelegateCommand CopyToClipboardButtonCommand { get; }

        #endregion

        #region Buttons methods

        /// <summary>
        /// Method for Cancel button on <see cref="Views.MessageBoxDialogView"/>.
        /// <para>Closes the UserControl and returns <see langword="false"/> to the message box's caller.</para>
        /// </summary>
        public static void CancelButton() => DialogHost.CloseDialogCommand.Execute(false, null);

        /// <summary>
        /// Method for Accept button on <see cref="Views.MessageBoxDialogView"/>.
        /// <para>Closes the UserControl and returns <see langword="true"/> to the message box's caller.</para>
        /// </summary>
        public static void AcceptButton() => DialogHost.CloseDialogCommand.Execute(true, null);

        /// <summary>
        /// Method for CopyToClipboard button on <see cref="Views.MessageBoxDialogView"/>.
        /// <para>Copies the message to clipboard.</para>
        /// </summary>
        public void CopyToClipboardButton() => Clipboard.SetText(Message);

        #endregion
    }
}
