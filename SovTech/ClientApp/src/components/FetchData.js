import React, { Component } from 'react';
import {
    InputGroup,
    InputGroupAddon,
    Input,
    Button} from 'reactstrap';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = {
            lists: { cnJokes: {}, swPeople: {} }, loading: true, search: ''
        };
        this.populateSearchData = this.populateSearchData.bind(this);
        this.updateInputValue = this.updateInputValue.bind(this);
    }

    componentDidMount() {
    }

    static renderSearchTables(lists) {
        return (
            <div className="row">
                <div className="col-md-6 col-sm-12 mt-1">
                    <h5>Skywalker n friends</h5>
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
                                {lists.swPeople.data.map((swap, index) =>
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
                </div>
                <div className="col-md-6 col-sm-12 mt-1">
                    <h5>Chuckles</h5>
                    <div className="table-responsive">
                        <table className='table table-striped' aria-labelledby="tabelLabel">
                            <thead>
                                <tr>
                                    <th>Joke</th>
                                </tr>
                            </thead>
                            <tbody>
                                {lists.cnJokes.data.map((joke, index) =>
                                    <tr key={index}>
                                        <td>{joke.value}</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                   
                </div>
            </div>

        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Awaiting search</em></p>
            : FetchData.renderSearchTables(this.state.lists);

        return (
            <div>
                <h1 id="tabelLabel" >Search Apis</h1>
                <InputGroup>
                    <Input type="text" value={this.state.search} onChange={this.updateInputValue} />
                    <InputGroupAddon addonType="append"><Button onClick={this.populateSearchData}>Search</Button></InputGroupAddon>
                </InputGroup>
                {contents}
            </div>
        );
    }

    async populateSearchData() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ Page: 1, PageSize: 100, Id: { "query": this.state.search } })
        };
        const response = await fetch('Search', requestOptions);
        const data = await response.json();
        this.setState({ lists: data.data, loading: false });
    }

    updateInputValue(evt) {
        this.setState({
            search: evt.target.value
        });
    }
}