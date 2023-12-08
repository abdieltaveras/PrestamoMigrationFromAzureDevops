﻿using System.Collections.Generic;

namespace UIClient.Models
{
    public interface ITreeItemData<T>
    {
        bool HasChild { get; }
        string Icon { get; }
        bool IsExpanded { get; set; }
        string Text { get; }
        HashSet<T> TreeItems { get; }
    }
}