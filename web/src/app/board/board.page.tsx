import React, { FunctionComponent } from "react";

import { useLoads } from "react-loads";
import Loader from 'react-loader-spinner'

import { LeaderboardEntryModel } from "./board.types";

const Board: FunctionComponent = () => {
  const {
    response,
    error,
    isPending,
    isResolved,
    isRejected
  } = useLoads('leaderboard', async () => {
    const rep = await fetch("https://localhost:5001/leaderboard");
    if (!rep.ok) {
      throw Error(rep.statusText);
    } else {
      return await rep.json() as LeaderboardEntryModel[];
    }
  });

  if (isPending) {
    return (<Loader
      type="Rings"
      color="blue" />);
  }

  if (isRejected) {
    return (<div>Error: {(error as Error).message}</div>);
  }

  if (isResolved) {
    return (
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>PR-Count</th>
          </tr>
        </thead>
        <tbody>
          {response?.map(entry => (
            <tr key={entry.name}>
              <td>{entry.name}</td>
              <td>{entry.prCount} / 4</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }

  return null;
}

export default Board;