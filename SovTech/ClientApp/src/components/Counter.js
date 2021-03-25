import React, { Component } from 'react';

export class Counter extends Component {
    static displayName = Counter.name;
    constructor(props) {
        super(props);
        this.state = { swaps: [], loading: true };
    }

    componentDidMount() {
        this.populateSwapiData();
    }

    static renderSwapiTable(swaps) {
        return (
            <div className="table-responsive">
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Mass</th>
                            <th>Height</th>
                            <th>Gender</th>
                            <th>Movie Count</th>
                        </tr>
                    </thead>
                    <tbody>
                        {swaps.map((swap, index) =>
                            <tr key={index}>
                                <td>{swap.name}</td>
                                <td>{swap.mass}</td>
                                <td>{swap.height}</td>
                                <td>{swap.gender}</td>
                                <td>{swap.films.length}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Counter.renderSwapiTable(this.state.swaps);

        return (
            <div>
                <h1 id="tabelLabel" >Skywalker n Friends</h1>
                {contents}
            </div>
        );
    }

    async populateSwapiData() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ Page: 1, PageSize: 100, Id: null })
        };
        const response = await fetch('Swapi', requestOptions);
        const data = await response.json();
        this.setState({ swaps: data.data, loading: false });
    }
}