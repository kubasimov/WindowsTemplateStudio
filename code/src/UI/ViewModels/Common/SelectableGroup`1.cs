﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.ObjectModel;
using Microsoft.Templates.UI.Mvvm;
using Microsoft.Templates.UI.Services;

namespace Microsoft.Templates.UI.ViewModels.Common
{
    public class SelectableGroup<T> : Observable
        where T : Selectable
    {
        private T _selected;
        private T _origSelected;
        private Func<bool> _isSelectionEnabled;
        private Action _onSelected;

        public T Selected
        {
            get => _selected;
            set => Select(value);
        }

        public ObservableCollection<T> Items { get; } = new ObservableCollection<T>();

        public SelectableGroup(Func<bool> isSelectionEnabled, Action onSelected = null)
        {
            _isSelectionEnabled = isSelectionEnabled;
            _onSelected = onSelected;
        }

        private bool Select(T value)
        {
            if (value != null)
            {
                _origSelected = _selected;
                if (value != _selected)
                {
                    _selected = value;
                }

                if (_isSelectionEnabled())
                {
                    foreach (var item in Items)
                    {
                        item.IsSelected = false;
                    }

                    _selected.IsSelected = true;
                    OnPropertyChanged(nameof(Selected));
                    _onSelected?.Invoke();
                    return true;
                }
                else
                {
                    DispatcherService.BeginInvoke(() =>
                    {
                        _selected = _origSelected;
                        OnPropertyChanged(nameof(Selected));
                    });
                }
            }

            return false;
        }
    }
}
