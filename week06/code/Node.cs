public class Node 
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // Problem 1: Insert Unique Values Only
        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else if (value > Data)  // Only insert if value is greater than current Data
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
        // If value == Data, do nothing (don't insert duplicates)
    }

    public bool Contains(int value)
    {
        // Problem 2: Contains
        if (value == Data)
        {
            return true;  // Found the value
        }
        else if (value < Data)
        {
            // Search in the left subtree
            if (Left is null)
                return false;
            else
                return Left.Contains(value);
        }
        else
        {
            // Search in the right subtree
            if (Right is null)
                return false;
            else
                return Right.Contains(value);
        }
    }

    public int GetHeight()
    {
        // Problem 4: Tree Height
        int leftHeight = 0;
        int rightHeight = 0;

        // Get height of left subtree
        if (Left is not null)
            leftHeight = Left.GetHeight();

        // Get height of right subtree
        if (Right is not null)
            rightHeight = Right.GetHeight();

        // Return 1 plus the maximum height of the two subtrees
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}