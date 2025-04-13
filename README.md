# Knight's Tour Problem

The Knight's Tour is a classic problem in computer science and mathematics, where the objective is to move a knight across a chessboard such that it visits every square exactly once. This repository offers an implementation of this problem in C#, utilizing the backtracking algorithm to find a solution.

![KnightsTour](https://github.com/user-attachments/assets/e18cb86e-6b26-4587-bdca-32e81b882fb9)

## Features

- **Backtracking Algorithm**: Employs a depth-first search approach to explore all possible knight moves, backtracking when a move leads to a dead end.
- **Chessboard Size Flexibility**: Allows users to define the dimensions of the chessboard, supporting various sizes beyond the standard 8x8.
- **Customizable Starting Position**: Enables users to set the knight's initial position on the board.

![image](https://github.com/user-attachments/assets/7b7c1feb-d612-4625-8746-f70b5d2539f9)

![image](https://github.com/user-attachments/assets/94d4b22d-8415-41bf-b5cf-de76001d3ef1)

## Getting Started

To run the Knight's Tour program:

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/AmirAbdollahi/knight-tour.git
   ```

2. **Open the Solution**:

   Navigate to the cloned directory and open `Knight Tour.sln` using Visual Studio or your preferred C# development environment.

3. **Build the Solution**:

   Compile the project to restore dependencies and build the executable.

4. **Run the Program**:

   Execute the program within your development environment or run the compiled executable from the command line.

## Usage

Upon running the program, it will prompt you to enter the dimensions of the chessboard and the starting position of the knight. The program will then attempt to find a solution to the Knight's Tour problem using the backtracking algorithm.

## Technical Explanation

### How the Algorithm Works

The knight moves in an L-shape: two squares in one direction and then one square perpendicular to that. There are 8 possible moves from any position on the board.

To simplify and generalize the movement logic, two arrays are used:

```csharp
int[] horizontal = { 2, 1, -1, -2, -2, -1, 1, 2 };
int[] vertical =   { -1, -2, -2, -1, 1, 2, 2, 1 };
```

These arrays represent the relative x and y changes for each of the knight’s 8 possible moves. For example:

- `horizontal[0] = 2` and `vertical[0] = -1` means moving two steps right and one step up.
- Each index corresponds to a specific knight move. Indexes 0 through 7 cover all the move patterns.

### Board Representation

The chessboard is represented as a 2D array:

```csharp
int[,] board = new int[8, 8];
```

Each cell on the board stores the move number when it was visited by the knight. A value of `false` means the cell has not yet been visited.

### Move Selection

For each position, the algorithm checks all possible knight moves using the `horizontal` and `vertical` arrays and selects the move that leads to the cell with the fewest onward options — that's Warnsdorff’s rule in action.

## License

This project is open-source and available under the MIT License.
