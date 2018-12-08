﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PropertyChangedCommandBehaviourTutorial
{
    public class PropertyChangedCommandBehavior : Behavior<DependencyObject>
    {
        public static readonly DependencyProperty PropertyChangedCommandProperty =
            DependencyProperty.Register("PropertyChangedCommand",
                typeof(ICommand), typeof(PropertyChangedCommandBehavior),
                new PropertyMetadata(null, ApplyChanged));

        public static readonly DependencyProperty DependencyPropertyNameProperty =
            DependencyProperty.Register("DependencyPropertyName",
                typeof(string), typeof(PropertyChangedCommandBehavior),
                new PropertyMetadata(string.Empty, ApplyChanged));

        private DependencyPropertyDescriptor descriptor;

        public ICommand PropertyChangedCommand
        {
            get => (ICommand) GetValue(PropertyChangedCommandProperty);
            set => SetValue(PropertyChangedCommandProperty, value);
        }

        public string DependencyPropertyName
        {
            get => (string) GetValue(DependencyPropertyNameProperty);
            set => SetValue(DependencyPropertyNameProperty, value);
        }

        private static void ApplyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as PropertyChangedCommandBehavior;
            behavior?.Setup();
        }


        protected override void OnAttached()
        {
            Setup();
        }

        protected override void OnDetaching()
        {
            if (descriptor != null && AssociatedObject != null)
                descriptor.RemoveValueChanged(AssociatedObject,
                    OnPropertyValueChanged);
        }

        private void Setup()
        {
            if (descriptor != null || string.IsNullOrEmpty(DependencyPropertyName)
                                   || AssociatedObject == null)
                return;
            descriptor = DependencyPropertyDescriptor.FromName(DependencyPropertyName,
                AssociatedObject.GetType(), AssociatedObject.GetType());
            descriptor?.AddValueChanged(AssociatedObject, OnPropertyValueChanged);
        }

        private void OnPropertyValueChanged(object sender, EventArgs e)
        {
            var value = AssociatedObject.GetValue(descriptor.DependencyProperty);
            if (PropertyChangedCommand == null ||
                !PropertyChangedCommand.CanExecute(value))
                return;
            PropertyChangedCommand.Execute(value);
        }
    }
}