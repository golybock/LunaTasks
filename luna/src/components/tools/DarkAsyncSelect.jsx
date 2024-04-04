import React from "react";
import AsyncSelect from "react-select/async";
import "./DarkAsyncSelect.css";

export default class DarkAsyncSelect extends React.Component {

	render() {
		return (<div>
			<AsyncSelect isMulti={this.props.isMulti}
						 cacheOptions
						 defaultOptions
						 className="react-select-container"
						 classNamePrefix="react-select"
						 value={this.props.value}
						 loadOptions={this.props.loadOptions}
						 onChange={(e) => this.props.onChange(e)}/>
		</div>)
	}

}