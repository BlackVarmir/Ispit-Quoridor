class Program
{
    const int boardSize = 9;
    static char[,] board = new char[boardSize, boardSize];
    static int player1Row = 0;
    static int player1Col = boardSize / 2;
    static int player2Row = boardSize - 1;
    static int player2Col = boardSize / 2;

    static void InitializeBoard()
    {
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if (row % 2 == 0 && col % 2 == 0)
                    board[row, col] = '+';
                else if (row % 2 == 0)
                    board[row, col] = '-';
                else if (col % 2 == 0)
                    board[row, col] = '|';
                else
                    board[row, col] = ' ';
            }
        }

        board[player1Row, player1Col] = '1';
        board[player2Row, player2Col] = '2';
    }

    static void DrawBoard()
    {
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                Console.Write(board[row, col] + " ");
            }
            Console.WriteLine();
        }
    }

    static void MovePlayer(int player)
    {
        Console.WriteLine("Введите направление перемещения (вверх - 'w', вниз - 's', влево - 'a', вправо - 'd'):");

        char direction = Convert.ToChar(Console.ReadLine());

        int newRow = 0;
        int newCol = 0;

        if (player == 1)
        {
            newRow = player1Row;
            newCol = player1Col;
        }
        else if (player == 2)
        {
            newRow = player2Row;
            newCol = player2Col;
        }

        switch (direction)
        {
            case 'w':
                newRow -= 2; 
                break;

            case 's':
                newRow += 2;
                break;

            case 'a':
                newCol -= 2;
                break;

            case 'd':
                newCol += 2; 
                break;

            default:
                Console.WriteLine("Некорректное направление. Попробуйте еще раз.");
                return;
        }

        if (IsMoveValid(newRow, newCol))
        {
            if (player == 1)
            {
                board[player1Row, player1Col] = ' ';
                player1Row = newRow;
                player1Col = newCol;
                board[player1Row, player1Col] = '1';
            }
            else if (player == 2)
            {
                board[player2Row, player2Col] = ' ';
                player2Row = newRow;
                player2Col = newCol;
                board[player2Row, player2Col] = '2';
            }
        }
        else
        {
            Console.WriteLine("Недопустимый ход. Попробуйте еще раз.");
        }
    }

    static bool IsMoveValid(int row, int col)
    {
        if (board[row, col] != ' ' || row % 2 != 0 || col % 2 != 0)
            return false;

        if (board[row, col] == ' ')
            return true;

        return false;
    }

    static void PlaceWall()
    {
        Console.WriteLine("Введите координаты ячейки, где хотите поставить стену (например, '2,3'):");
        string input = Console.ReadLine();
        string[] coordinates = input.Split(',');

        int row = Convert.ToInt32(coordinates[0]) * 2; 
        int col = Convert.ToInt32(coordinates[1]) * 2; 

        if (IsWallPlacementValid(row, col))
        {
            board[row, col] = '#';
        }
        else
        {
            Console.WriteLine("Недопустимое размещение стены. Попробуйте еще раз.");
        }
    }

    static bool IsWallPlacementValid(int row, int col)
    {
        if (board[row, col] != ' ' || row % 2 != 1 || col % 2 != 1)
            return false;

        if (row % 2 == 0 && col % 2 == 0)
            return false;

        if (board[row, col] != ' ')
            return false;

        if (row % 2 == 1 && col % 2 == 1)
        {
            if (board[row - 1, col] == '#' && board[row + 1, col] == '#')
                return false;
        }
        else if (row % 2 == 0 && col % 2 == 1)
        {
            if (board[row, col - 1] == '#' && board[row, col + 1] == '#')
                return false;
        }
        else if (row % 2 == 1 && col % 2 == 0)
        {
            if (board[row - 1, col] == '#' && board[row + 1, col] == '#')

                return false;
        }

        return true;
    }

    static void Main()
    {
        InitializeBoard();
        bool gameOver = false;
        int currentPlayer = 1;

        while (!gameOver)
        {
            DrawBoard();
            Console.WriteLine("Игрок " + currentPlayer + ", ваш ход.");

            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Переместиться");
            Console.WriteLine("2. Поставить стену");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    MovePlayer(currentPlayer);
                    break;

                case 2:
                    PlaceWall();
                    break;

                default:
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                    continue;
            }

            if (currentPlayer == 1)
                currentPlayer = 2;
            else
                currentPlayer = 1;

            if (player1Row == boardSize - 1)
            {
                Console.WriteLine("Игрок 1 победил!");
                gameOver = true;
            }
            else if (player2Row == 0)
            {
                Console.WriteLine("Игрок 2 победил!");
                gameOver = true;
            }
        }

        Console.ReadLine();
    }
}
