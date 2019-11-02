using System;
using System.Collections.Generic;
using System.Linq;

namespace Task
{
    class Program
    {
        public static void Main(string[] args)
        {
            Random random = new Random();
            UnitRenderer unitRenderer = new UnitRenderer();

            Unit[] units = new Unit[3];
            units[0] = new Unit(5, 5, '0');
            units[1] = new Unit(10, 10, '1');
            units[2] = new Unit(15, 15, '2');

            while (true)
            {
                foreach (Unit unit in units)
                {
                    unit.Move(random.Next(-1, 1), random.Next(-1, 1));
                }
                unitRenderer.RenderUnits(units);
            }
        }
    }

    public class Unit
    {
        private int _x;
        private int _y;
        private bool _isAlive = true;
        private char _model;

        public int X { get => _x; }
        public int Y { get => _y; }
        public bool IsAlive { get => _isAlive; }
        public char Model { get => _model; }

        public Unit(int x, int y, char model)
        {
            _x = x;
            _y = y;
            _model = model;
        }

        public void Move(int x, int y)
        {
            if (_x + x >= 0)
                _x += x;

            if (_y + y >= 0)
                _y += y;
        }

        public void Die()
        {
            _isAlive = false;
        }
    }

    public class UnitRenderer
    {
        public void RenderUnits(Unit[] units)
        {
            for (int i = 0; i < units.Length; i++)
            {
                RenderUnit(units[i]);
            }
        }

        public void RenderUnit(Unit unit)
        {
            if (unit.IsAlive)
            {
                Console.SetCursorPosition(unit.X, unit.Y);
                Console.Write(unit.Model);
            }
        }
    }
}