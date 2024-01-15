namespace Maze
{
    public class MazeCell
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        //public bool Visited { get; set; }
        //public bool Locked { get; set; }
        public bool NorthBlocked { get; set; }
        public bool SouthBlocked { get; set; }
        public bool WestBlocked { get; set; }
        public bool EastBlocked { get; set; }

        public bool Banned { get; set; }

        public EDirection NewDirection { get; set; } = EDirection.None;

        public MazePlayer APIHelper { get; set; }

        public MazeCell(MazePlayer aPIHelper)
        {

            APIHelper = aPIHelper;
        }

        public bool CanGoNorth()
        {
            if (APIHelper.LastDirection == EDirection.South)
            {
                return false;
            }
            if (NorthBlocked)
            {
                return false;
            }
            else
            {
                var northCell = GetNorthCell();
                if (northCell == null)
                {
                    return true;
                }
                else
                {
                    if (northCell.Banned)
                    {
                        return false;
                    }
                    else
                    {
                        //use reference of previous cell
                        if (APIHelper.CurrentPath.Contains(northCell)
                                && northCell != APIHelper.CurrentPath.Last())
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
        }

        public MazeCell? GetNorthCell()
        {
            if (PositionY > 0)
            {
                return APIHelper.MazeCells[PositionX, PositionY - 1];
            }
            return null;
        }

        public bool CanGoSouth()
        {
            if (APIHelper.LastDirection == EDirection.North)
            {
                return false;
            }
            if (SouthBlocked)
            {
                return false;
            }
            else
            {
                var southCell = GetSouthCell();
                if (southCell == null)
                {
                    return true;
                }
                else
                {
                    if (southCell.Banned)
                    {
                        return false;
                    }
                    else
                    {
                        //use reference of previous cell
                        if (APIHelper.CurrentPath.Contains(southCell)
                                && southCell != APIHelper.CurrentPath.Last())
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
        }

        public MazeCell? GetSouthCell()
        {
            if (PositionY < APIHelper.Height - 1)
            {
                return APIHelper.MazeCells[PositionX, PositionY + 1];
            }
            return null;
        }

        public bool CanGoEast()
        {
            if (APIHelper.LastDirection == EDirection.West)
            {
                return false;
            }
            if (EastBlocked)
            {
                return false;
            }
            else
            {
                var eastCell = GetEastCell();
                if (eastCell == null)
                {
                    return true;
                }
                else
                {
                    if (eastCell.Banned)
                    {
                        return false;
                    }
                    else
                    {
                        //use reference of previous cell
                        if (APIHelper.CurrentPath.Contains(eastCell)
                                && eastCell != APIHelper.CurrentPath.Last())
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
        }

        public MazeCell? GetEastCell()
        {
            if (PositionX < APIHelper.Width - 1)
            {
                return APIHelper.MazeCells[PositionX + 1, PositionY];
            }
            return null;
        }

        public bool CanGoWest()
        {
            if (APIHelper.LastDirection == EDirection.East)
            {
                return false;
            }

            if (WestBlocked)
            {
                return false;
            }
            else
            {
                var westCell = GetWestCell();
                if (westCell == null)
                {
                    return true;
                }
                else
                {
                    if (westCell.Banned)
                    {
                        return false;
                    }
                    else
                    {
                        //use reference of previous cell
                        if (APIHelper.CurrentPath.Contains(westCell)
                                && westCell != APIHelper.CurrentPath[APIHelper.CurrentPath.Count - 1])
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
        }

        public MazeCell? GetWestCell()
        {
            if (PositionX > 0)
            {
                return APIHelper.MazeCells[PositionX - 1, PositionY];
            }
            return null;
        }
        public override string ToString()
        {
            return $"{PositionX},{PositionY},{EastBlocked},{SouthBlocked},{WestBlocked},{NorthBlocked}, {Enum.GetName(typeof(EDirection), NewDirection)}";
        }
    }
}
