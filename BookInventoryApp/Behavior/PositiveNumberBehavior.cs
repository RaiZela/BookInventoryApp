namespace BookInventoryApp.Behavior;

public class PositiveNumberBehavior : Behavior<Entry>
{
    protected override void OnAttachedTo(Entry entry)
    {
        base.OnAttachedTo(entry);
        entry.TextChanged += OnEntryTextChanged;
    }
    protected override void OnDetachingFrom(Entry entry)
    {
        base.OnDetachingFrom(entry);
        entry.TextChanged -= OnEntryTextChanged;
    }
    private void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is Entry entry && !string.IsNullOrEmpty(entry.Text))
        {
            if (!double.TryParse(entry.Text, out double value) || value < 0)
            {
                entry.TextColor = Colors.Red;
            }
            else
            {
                entry.TextColor = Colors.White;
            }
        }
    }
}
