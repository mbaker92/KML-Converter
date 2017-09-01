#pragma once


/// Function prototypes
vector<string> get_header_list(fstream & CSV_fh);
void strip_KML(fstream & CSV_fh, fstream & new_KML_fh, fstream & orig_KML_fh, vector<int> & unique_header_list);

bool vec_contains(vector<int> in_vec, int val);
long long count_CSV_records(fstream & CSV_fh);
void get_CSV_records(fstream & CSV_fh, string ** entry_list, vector<int> & unique_header_list);
bool entry_matches(string ** entry_list, vector<string> entry, long long entry_count, long long & last_pos);

/// Function Definitions
vector<string> get_header_list(fstream & CSV_fh) {
	// strips a header vector from the first line of the CSV
	vector<string> header_list;
	CSV_fh.clear();
	CSV_fh.seekp(0);
	string readline;
	string readword = "";
	getline(CSV_fh, readline);

	readline += '\0';
	for (int i = 0; i < readline.length(); i++) {
		if ((readline[i] == ',') || (readline[i] == '\r') || (readline[i] == '\n') || (readline[i] == '\0')) {
			header_list.push_back(readword);
			readword.clear();
		}
		else
			readword += readline[i];
	}
	
	return header_list;
}
void strip_KML(fstream & CSV_fh, fstream & new_KML_fh, fstream & orig_KML_fh, vector<int> & unique_header_list) {
	bool data_zone = false; 
	string readline = ""; 
	string short_readline;
	long long entry_pos = 0; 
	long long temp_entry_pos = 0;
	long long CSV_entries = count_CSV_records(CSV_fh);

	int header_num = 0;
	long long cur_ent = 0; 
	
	string dummystring;
	string fullentry;

	string ** entries_to_keep = new string *[CSV_entries];
	get_CSV_records(CSV_fh, entries_to_keep, unique_header_list);

	// test section for get_CSV_records function
	/*
	for (int i = 0; i < CSV_entries; i++) {
		for (int j = 0; j < unique_header_list.size(); j++) {
			cout << entries_to_keep[i][j] << '\t';
		}
		cout << endl;
	}
	*/


	orig_KML_fh.clear(); 
	orig_KML_fh.seekg(0);

	vector<string> entry_values;
	vector<string> headers = get_header_list(CSV_fh);
	CSV_fh.clear();
	CSV_fh.seekg(0);
	
	//cout << CSV_entries << endl;

	while (1) {
		if (!getline(orig_KML_fh, readline)) {
			break;
		}
		entry_values.clear();
		short_readline = readline;
		trim_tab_space(short_readline);
		if (beginswith(short_readline, entry_indicator)) {
			data_zone = true;
			fullentry = readline;
		}
		if (beginswith(short_readline, end_entry_indicator)) {
			data_zone = false;
		}
		
		if (!data_zone) {
			// orig_KML_fh is not in placemark region
			// move all data over, to preserve formatting, headers, etc
			new_KML_fh << readline;
		}
		else {
			// orig_KML_fh is in placemark region
			header_num = 0;
			while (getline(orig_KML_fh, readline)) {
				fullentry += readline;
				short_readline = readline;
				trim_tab_space(short_readline);
				
				if (beginswith(short_readline, end_entry_indicator)) {
					data_zone = false;
					break;
				}
				if (beginswith(short_readline, entry_name)) {
					//cout << short_readline << endl;
					if (vec_contains(unique_header_list, header_num)) {
						entry_values.push_back(read_unbracketed(short_readline));
					}
					header_num++;
				}
			}

			//
			if (entry_values.size() == unique_header_list.size()) {
				if (entry_matches(entries_to_keep, entry_values, CSV_entries, cur_ent)) {
					new_KML_fh << fullentry;
					
				}
			}
			else {
				cout << "failed entry" << endl; 
			}
		}
	}


	for (int i = 0; i < CSV_entries; i++)
		delete[] entries_to_keep[i];
	delete[] entries_to_keep;
}

bool vec_contains(vector<int> in_vec, int val) {
	for (int i = 0; i < in_vec.size(); i++)
		if (in_vec.at(i) == val)
			return true;
	return false;
}
long long count_CSV_records(fstream & CSV_fh) {
	CSV_fh.clear();
	CSV_fh.seekg(0);

	string readline;

	long long rows = 0; 
	while (getline(CSV_fh, readline))
		rows++;
	CSV_fh.clear();
	CSV_fh.seekg(0);
	return rows - 1;
}
void get_CSV_records(fstream & CSV_fh, string ** entry_list, vector<int> & unique_header_list) {
	CSV_fh.clear();
	CSV_fh.seekg(0);

	int header_pos = 0; 
	int header_list_pos = 0;
	int entry_list_iter = 0;

	string readline;
	string readword = "";
	getline(CSV_fh, readline); // skip headers
	while (getline(CSV_fh, readline)) {
		readline += ',';
		entry_list[entry_list_iter] = new string[unique_header_list.size()];
		for (int i = 0; i < readline.size(); i++) {
			
			if ((readline[i] == ',') && (header_list_pos <unique_header_list.size())) {
				//cout << "word: " << readword << " pos: " << header_list_pos << endl;
				if (header_pos == unique_header_list.at(header_list_pos)) {
					entry_list[entry_list_iter][header_list_pos] = readword;
					header_list_pos++;
				}
				header_pos++;
				readword.clear();
			}
			else {
				readword += readline[i];
			}
		}
		readword.clear();
		header_pos = 0;
		header_list_pos = 0;
		entry_list_iter++;
	}
	CSV_fh.clear();
	CSV_fh.seekg(0);
}
bool entry_matches(string ** entry_list, vector<string> entry, long long entry_count, long long & last_pos) {

	bool match_flag;
	for (int i = last_pos; i < entry_count; ) {
		match_flag = true;
		for (int j = 0; j < entry.size(); j++) {
			if (entry_list[i][j] != entry[j]) {
				match_flag = false;
				break; 
			}
		}
		if (match_flag) {
			last_pos = i;
			return true;
		}
		i++;
		if ((i == entry_count) && (last_pos != 0 )){
			i = 0;
			entry_count = last_pos;
			last_pos = 0;
		}
	}
	return false;


}

/*
int last_pos = 5;
int entry_count = 10;
for (int i = last_pos; i < entry_count; ) {
	cout << i << endl;
	i++;
	if ((i == entry_count) && (last_pos != 0 )) {
		i = 0;
		entry_count = last_pos;
		last_pos = 0;
	}
}
system("pause");
*/