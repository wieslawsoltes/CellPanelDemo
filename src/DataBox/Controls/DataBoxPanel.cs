﻿using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using DataBox.Primitives.Layout;

namespace DataBox.Controls;

public class DataBoxPanel : VirtualizingStackPanel
{
    internal DataBox? DataBox { get; set; }

    protected override Type StyleKeyOverride => typeof(DataBoxPanel);

    public override void ApplyTemplate()
    {
        base.ApplyTemplate();

        DataBox = this.GetLogicalAncestors().FirstOrDefault(x => x is DataBox) as DataBox;
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        if (DataBox is null)
        {
            return availableSize;
        }

        var children = GetRealizedContainers();

        if (children is null)
        {
            availableSize  = base.MeasureOverride(availableSize);
            children = GetRealizedContainers();
        }

        return children is { } 
            ? DataBoxRowsLayout.Measure(availableSize, DataBox, base.MeasureOverride, base.InvalidateMeasure, children) 
            : availableSize;
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        // TODO:
        // finalSize  = base.ArrangeOverride(finalSize);

        if (DataBox is null)
        {
            return finalSize;
        }

        var children = GetRealizedContainers();
        return children is { } 
            ? DataBoxRowsLayout.Arrange(finalSize, DataBox, base.ArrangeOverride, children)
            : finalSize;
    }
}
