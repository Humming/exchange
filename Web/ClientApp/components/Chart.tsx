import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';
import * as ChartStore from '../store/Chart';
import {
    BarSeries,
    CandlestickSeries,
    } from "react-stockcharts/lib/series";
type ChartProps =
    ChartStore.ChartState
    & typeof ChartStore.actionCreators
    & RouteComponentProps<{ startDateIndex: string }>;

class Chart extends React.Component<ChartProps, {}> {
    public render() {
        return <div>
            <h1>Charts</h1>
            {this.renderChartDataTable()}
            <div id="chart_div"></div>
        </div>;
    }

    componentWillMount() {
        let startDateIndex = parseInt(this.props.match.params.startDateIndex) || 0;
        this.props.requestChartData(startDateIndex);
    }
    componentWillReceiveProps(nextProps: ChartProps) {
        let startDateIndex = parseInt(nextProps.match.params.startDateIndex) || 0;
        this.props.requestChartData(startDateIndex);
    }



    private renderChartDataTable() {
        return <table className='table'>
                   <thead>
                   <tr>
                       <th>Date</th>
                       <th>Open</th>
                       <th>High</th>
                       <th>Low</th>
                       <th>Close</th>
                       <th>Volume</th>
                   </tr>
                   </thead>
                   <tbody>
                   {this.props.chartData.map(cd =>
                       <tr key={cd.date}>
                           <td>{cd.date}</td>
                           <td>{cd.open}</td>
                           <td>{cd.high}</td>
                           <td>{cd.low}</td>
                           <td>{cd.close}</td>
                           <td>{cd.volume}</td>
                       </tr>
                   )}
                   </tbody>
               </table>;
    }
}


export default connect(
    (state: ApplicationState) => state.chart, // Selects which state properties are merged into the component's props
    ChartStore.actionCreators                 // Selects which action creators are merged into the component's props
)(Chart) as typeof Chart;

