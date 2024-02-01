async function handleErrors(response) {
    if (!response.ok) {
        const contentType = response.headers.get("content-type");
        if (contentType && contentType.indexOf("application/json") !== -1) {
            const data = await response.json();
            throw data;
        } else {
            const data = await response.text();
            throw data;
        }
    }
    return response;
}

const boardLoadedWithShips = (shipsCoord) => {
    let board = createEmptyBoard();
    for (const ship of shipsCoord) {
        board[ship.row][ship.column] = ship.hit ? "Y" : "NShip";
    }
    console.log(board);
    return board;
};

const boardLoadedWithMoves = (board, moves) => {
    // console.log("Board load games moves", moves);
    let copie = board.slice();
    for (const move of moves) {
        copie[move.row][move.column] = move.result
    }
    return copie;
};

// same as below ;)
function convertIndexesToApi([indexX, indexY]) {
    return { Row: indexX, Column: indexY };
}

// weird behavior but works well with my js knowledge :))
function convertIndexesFromApi(x, y) {
    return [x, y];
}

const shipConfigationForApi = (row, column, shipLength, isVertical) => {
    return {
        Row: row,
        Column: column,
        Length: shipLength,
        IsVertical: isVertical,
    };
};

const createEmptyBoard = () =>
    Array(10)
        .fill(0)
        .map((row) => new Array(10).fill(0));

// check also is selected and placeOnGameTable is a valid input (aka not null)
const mutarePermisa = (board, ships, selected, placeOnGameTable) => {
    if (selected === null) {
        return false;
    }
    if (!placeOnGameTable) {
        return false;
    }
    const indexX = placeOnGameTable[0],
        indexY = placeOnGameTable[1];
    const ship = ships.find((ship) => ship.name === selected);

    if (ship.isVertical) {
        return (
            indexX + ship.length <= 10 && checkOtherShips(board, ship, indexX, indexY)
        );
    } else {
        return (
            indexY + ship.length <= 10 && checkOtherShips(board, ship, indexX, indexY)
        );
    }
};

const createBoardWithShip = (board, ship, value, [indexX, indexY]) => {
    let copie = board.slice();
    if (ship.isVertical) {
        for (let i = indexX; i < indexX + ship.length; i++) {
            copie[i][indexY] = value;
        }
    } else {
        for (let i = indexY; i < indexY + ship.length; i++) {
            copie[indexX][i] = value;
        }
    }
    console.log(copie);
    return copie;
};

const checkOtherShips = (board, ship, startX, startY) => {
    const { isVertical, length } = ship;
    const maxIndex = isVertical ? startX + length : startY + length;

    if (maxIndex > 10) {
        return false;
    }

    for (let i = 0; i < length; i++) {
        const x = isVertical ? startX + i : startX;
        const y = isVertical ? startY : startY + i;

        if (board[x][y] !== 0) {
            return false;
        }
    }

    return true;
};

const checkWinnerWithMoves = (moves, needed) => {
    let uniqueValues = new Set();
    for (const move of moves) {
        uniqueValues.add([move.x, move.y]);
    }
    return uniqueValues.length === needed;
};

const AVAILABLE_SHIPS = [
    {
        name: "carrier",
        length: 6,
        placed: null,
        isVertical: true,
        row: null,
        column: null,
    },
    {
        name: "battleship",
        length: 4,
        placed: null,
        isVertical: false,
        row: null,
        column: null,
    },
    /*{
      name: "battleship1",
      length: 4,
      placed: null,
      vertical: false,
      x: null,
      y: null,
    },
    {
      name: "submarine",
      length: 3,
      placed: null,
      vertical: false,
      x: null,
      y: null,
    },
    {
      name: "submarine1",
      length: 3,
      placed: null,
      vertical: false,
      x: null,
      y: null,
    },
    {
      name: "submarine2",
      length: 3,
      placed: null,
      vertical: false,
      x: null,
      y: null,
    },
    {
      name: "destroyer",
      length: 2,
      placed: null,
      vertical: false,
      x: null,
      y: null,
    },
    {
      name: "destroyer1",
      length: 2,
      placed: null,
      vertical: false,
      x: null,
      y: null,
    },
    {
      name: "destroyer2",
      length: 2,
      placed: null,
      vertical: false,
      x: null,
      y: null,
    },
    {
      name: "destroyer3",
      length: 2,
      placed: null,
      vertical: false,
      x: null,
      y: null,
    },*/
];

const convertGameState = (gameState) => {
    if (gameState === 0) {
        return "CREATED";
    } else if (gameState === 1) {
        return "MAP_CONFIG";
    } else if (gameState === 2) {
        return "ACTIVE";
    } else if (gameState === 3) {
        return "FINISHED";
    }
};


export {
    AVAILABLE_SHIPS,
    checkWinnerWithMoves,
    createEmptyBoard,
    checkOtherShips,
    mutarePermisa,
    createBoardWithShip,
    shipConfigationForApi,
    handleErrors,
    boardLoadedWithMoves,
    convertIndexesFromApi,
    convertIndexesToApi,
    boardLoadedWithShips,
    convertGameState,
};
