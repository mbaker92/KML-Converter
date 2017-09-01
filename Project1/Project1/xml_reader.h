#pragma once

#define header_info_file_name "header_info.KML"

#define fields_header "<Schema"
#define entry_indicator "<Placemark>"
#define end_entry_indicator "</Placemark>"
#define field_entry "<SimpleField"
#define end_field_entry "</Schema"
#define field_name " name="
#define entry_name "<SimpleData"

using namespace std;

/// Function Prototypes

void leave_header_info(fstream & KML_fh);
vector<string> create_CSV_headers(fstream & CSV_fh, fstream & KML_fh);
void create_CSV_entries(fstream & CSV_fh, fstream & KML_fh, vector<string> headerlist);


int print_KML_to_terminal(fstream & filehandle);
bool trim_tab_space(string & in_string);
bool trim_to_string(string & longer, string shorter);
string read_quoted(string in_string);
string read_unbracketed(string in_string);
bool beginswith(string longer, string shorter);


/// Function Definitions


void leave_header_info(fstream & KML_fh) {
	// produces a copy of the header information and leaves it in a file
	// determines headed to end at first "Placemark" indicator
	fstream headerinfo_fh;
	KML_fh.seekp(0);
	headerinfo_fh.open(header_info_file_name, fstream::out);
	
	string readline;
	while (getline(KML_fh, readline)) {
		trim_tab_space(readline);
		if (beginswith(readline, entry_indicator))
			break;
		headerinfo_fh << readline;
	}
	KML_fh.seekp(0);
	headerinfo_fh.close();
}
vector<string> create_CSV_headers(fstream & CSV_fh, fstream & KML_fh) {
	// reads the header information from the KML file and puts those headers in the CSV
	string readline;
	string headername;
	vector<string> headerlist;
	KML_fh.seekp(0);
	while (getline(KML_fh, readline)) {
		trim_tab_space(readline);
		if (beginswith(readline, fields_header))
			break;
	}
	while (1) {
		getline(KML_fh, readline);
		trim_tab_space(readline);
		if (beginswith(readline, field_entry)) {
			trim_to_string(readline, field_name);
			headername += read_quoted(readline);
			headername += ",";
			headerlist.push_back( read_quoted(readline));
		}
		else if (beginswith(readline, end_field_entry)) {
			break;
		}
	}
	headername.erase(headername.length() - 1, 1);
	CSV_fh << headername << endl;
	return headerlist;
}
void create_CSV_entries(fstream & CSV_fh, fstream & KML_fh, vector<string> headerlist) {
	
	string readline;
	string out_line;
	string data_entry = entry_name;
	int entry_num = 0;
	long long seek_pos = 0;
	data_entry += field_name;
	bool line_flag = false;
	
	KML_fh.seekg(0);

	long long iter = 0;

	while (getline(KML_fh, readline)) {
		trim_tab_space(readline);
		if (beginswith(readline, entry_indicator)) {
			// filehandle has entered the correct domain
			// record position and set line_flag
			line_flag = true;
			seek_pos = KML_fh.tellg();
			out_line = "";
			//cout << seek_pos << endl;
		}
		if (beginswith(readline, end_entry_indicator)) {
			if (entry_num != (headerlist.size() )) {
				// entire entry searched, data value not found
				out_line += ",";
				KML_fh.seekg(seek_pos);
				entry_num++;
			}
			else {
				// entire entry searched, all data values found
				line_flag = false;
				CSV_fh << out_line << endl;
				out_line = "";
				entry_num = 0;
			}
		}
		if (line_flag && (entry_num < headerlist.size())) {
			if (beginswith(readline, entry_name)) {
				if (!(headerlist.at(entry_num).compare(read_quoted(readline)))) {
					// append the data to the out_line
					// increment entry_num to search for next entry in headerlist
					entry_num++;
					out_line += read_unbracketed(readline);
					out_line += ",";
				}
			}
		}
	}
}

bool beginswith(string longer, string shorter) {
	// checks to see if one strings begins with another
	return (longer.compare(0, shorter.length(), shorter) == 0);
}
bool trim_tab_space(string & in_string) {
	// removes all tab and space from the beginning of a string
	while ((in_string[0] == ' ') ||
		(in_string[0] == '\t'))
		in_string.erase(0, 1);
	return true;
}
bool trim_to_string(string & longer, string shorter) {
	int i = 0; 
	if (longer.find(shorter) == string::npos)
		return false;
	longer.erase(0, longer.find(shorter) + shorter.length());
	return true;
}
string read_quoted(string in_string) {
	// reads forward to the first quote then reads the quoted material
	string out_string; 
	bool quote_detected = false;
	for (int i = 0; i < in_string.length(); i++) {
		if (in_string[i] == '"') {
			if (quote_detected == true)
				break;
			else
				quote_detected = true;
			i++;
		}
		if (quote_detected)
			out_string += in_string[i];
	}
	return out_string;
}
string read_unbracketed(string in_string) {
	// reads forward to the first unbracketed information
	// returns first unbracketed information

	string out_string;
	int bracketed = 0;
	for (int i = 0; i < in_string.length(); i++) {
		if (in_string[i] == '<')
			bracketed += 1;
		else if (in_string[i] == '>')
			bracketed -= 1;
		else {
			if ((out_string.length() > 0) && (bracketed > 0))
				break;
			if (bracketed == 0)
				out_string += in_string[i];
		}
	}
	return out_string;
}
int print_KML_to_terminal(fstream & filehandle) {
	// for debugging purposes, dumps an entire filehandle to terminal
	string readline;
	filehandle.clear();
	filehandle.seekg(0);

	while (getline(filehandle, readline)) {
		cout << readline << endl;
	}
	return 1;
}
