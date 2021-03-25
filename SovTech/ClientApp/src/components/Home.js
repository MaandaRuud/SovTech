import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { cats: [], loading: true };
    }

    componentDidMount() {
        this.populateChuckData();
    }

    static renderChuckTable(cats) {
        return (
            <div className="table-responsive">
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Category</th>
                        </tr>
                    </thead>
                    <tbody>
                        {cats.map((cat, index) =>
                            <tr key={index}>
                                <td>{cat}</td>
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
            : Home.renderChuckTable(this.state.cats);

        return (
            <div>
                <h1 id="tabelLabel" >Walker Texas Categories</h1>
                {contents}
            </div>
        );
    }

    async populateChuckData() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ Page: 1, PageSize: 100, Id: null })
        };
        const response = await fetch('chuck', requestOptions);
        const data = await response.json();
        this.setState({ cats: data.data, loading: false });
    }
}