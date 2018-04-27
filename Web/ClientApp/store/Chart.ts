import { fetch, addTask } from 'domain-task';
import { Action, Reducer, ActionCreator } from 'redux';
import { AppThunkAction } from './';


// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface ChartState {
    isLoading: boolean;
    startDateIndex?: number;
    chartData: ChartData[];
}

export interface ChartData {
    date: string;
    open: number;
    high: number;
    low: number;
    close: number;
    volume: number;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.
// Use @typeName and isActionType for type detection that works even after serialization/deserialization.

interface RequestChartData { type: 'REQUEST_CHARTDATA'; startDateIndex: number;}
interface ReceiveChartData { type: 'RECEIVE_CHARTDATA'; startDateIndex: number; chartData: ChartData[]; }

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestChartData | ReceiveChartData;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestChartData: (startDateIndex: number): AppThunkAction <KnownAction> => (dispatch, getState) => {
        if (startDateIndex !== getState().chart.startDateIndex) {
            let fetchTask = fetch(`api/SampleData/GetChartData?startDateIndex=${startDateIndex }`)
                .then(response => response.json() as Promise<ChartData[]>)
                .then(data => {
                    dispatch({
                        type: 'RECEIVE_CHARTDATA',
                        startDateIndex: startDateIndex,
                        chartData: data
                    });
                });
            addTask(fetchTask);
            dispatch({ type: 'REQUEST_CHARTDATA',startDateIndex: startDateIndex });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: ChartState = { chartData: [], isLoading: false };


export const reducer: Reducer<ChartState> = (state: ChartState, incomingAction: Action) => {
    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_CHARTDATA':
            return {
                startDateIndex: action.startDateIndex,
                chartData: state.chartData,
                isLoading: true
            };
        case 'RECEIVE_CHARTDATA':
            if (action.startDateIndex === state.startDateIndex) {
                return { startDateIndex: action.startDateIndex, chartData: action.chartData, isLoading: false };
            }
            break;
        default:
            // The following line guarantees that every action in the KnownAction union has been covered by a case above
            const exhaustiveCheck: never = action;
    }

    // For unrecognized actions (or in cases where actions have no effect), must return the existing state
    //  (or default initial state if none was supplied)
    return state || unloadedState;
};
