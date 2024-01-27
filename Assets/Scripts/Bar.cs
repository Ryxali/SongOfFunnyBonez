[System.Serializable]
public struct Bar
{
    public BarBeat this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return beat0;
                case 1:
                    return beat1;
                case 2:
                    return beat2;
                case 3:
                    return beat3;
                default:
                    throw new System.IndexOutOfRangeException();
            }
        }
    }
    public BarBeat beat0;
    public BarBeat beat1;
    public BarBeat beat2;
    public BarBeat beat3;

}
