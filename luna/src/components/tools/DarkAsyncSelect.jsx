import React from "react";
import AsyncSelect from "react-select/async";
import "./DarkAsyncSelect.css";

const customStyles = {
	control: (base, state) => ({
		...base,
		background: "#212529",
		// match with the menu
		// borderRadius: state.isFocused ? "3px 3px 0 0" : 3,
		// Overwrittes the different states of border
		borderColor: state.isFocused ? "#7B7B7B;" : "#7B7B7B;",
		// Removes weird border around container
		boxShadow: state.isFocused ? null : null,
	}),
	menu: base => ({
		...base,
		background: "#212529",
		marginTop: 0,
		"&:hover": {
			background: "#1D1D1D"
		}
	}),
	menuList: base => ({
		...base,
		background: "#212529",
	}),
	multiValue: base => ({
		backgroundColor: "#212529",
	}),
	multiValueLabel: base => ({
		color: "white",
		"&:hover": {
			backgroundColor: "#1D1D1D",
			color: "white"
		}
	}),
	option:base => ({
		"&:active": {
			...base[':active'],
			backgroundColor: "#1D1D1D",
			color: "white"
		}
	})
};

export default class DarkAsyncSelect extends React.Component {

	render() {
		return (<div>
			<AsyncSelect  isMulti={this.props.isMulti}
						 cacheOptions
						 defaultOptions
						 // styles={customStyles}
						 className="react-select-container"
						 classNamePrefix="react-select"
						 value={this.props.value}
						 loadOptions={this.props.loadOptions}
						 onChange={(e) => this.props.onChange(e)}/>
		</div>)
	}

}