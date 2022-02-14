#include <stdio.h>
#include <stdlib.h>

#define NAME_LEN 80
#define RACE_NAME_LEN 100
#define GARBAGE_BUFFER_LEN 50

typedef struct {
    int id;
    int year;
    int first_race;
    int races_cnt;
    char race_director[NAME_LEN];
    char tyre_supplier[NAME_LEN];
} t_season;

typedef struct {
    int id;
    int season_id;
    int lap_count;
    int lap_length;
    int next_race;
    int empty;
    int offset;
    char name[RACE_NAME_LEN];
} t_race;

typedef struct {
    int n;
    t_race* array;
} array_pair;

FILE *fseasons_ind;
FILE *fseasons;
FILE *fraces;
FILE *fgarbage;

t_season* get_m(int id) {
    if (id < 1) {
        return NULL;
    }
    fseek(fseasons_ind, 0L, SEEK_END);
    if (ftell(fseasons_ind) <= sizeof(int)*(id-1)) {
        return NULL;
    }
    fseek(fseasons_ind, sizeof(int)*(id-1), SEEK_SET);
    int offset;
    fread(&offset, sizeof(int), 1, fseasons_ind);
    t_season* season = (t_season*)malloc(sizeof(t_season));
    if (offset < 0) {
        return NULL;
    }
    fseek(fseasons, offset, SEEK_SET);
    fread(season, sizeof(t_season), 1, fseasons);
    return season;
}

t_season* get_m_ui() {
    int id;
    printf("enter season id: ");
    scanf("%d", &id);
    t_season* season = get_m(id);
    if (season == NULL) {
        printf("this season doesn`t exist.\n");
    } else {
        printf("-----------\n");
        printf("season id: %d\nyear: %d\nrace director: %s\ntyre supplier: %s\nraces: %d\n", 
        season->id, season->year, season->race_director, season->tyre_supplier, season->races_cnt);
    }
    return season;
}

array_pair get_s(t_season* season, int race_id) {
    array_pair result;
    int n = season->races_cnt;
    if (n == 0) {
        result.n = 0;
        result.array = NULL;
        return result;
    }
    t_race* races = (t_race*)malloc(n*sizeof(t_race));
    result.n = n;
    result.array = races;
    if (race_id != 0) {
        result.n = -1;
    }
    int next_race = season->first_race;
    int i = 0;
    while (next_race != -1) {
        fseek(fraces, next_race, SEEK_SET);
        t_race tmp;
        fread(&tmp, sizeof(t_race), 1, fraces);
        next_race = tmp.next_race;
        if (!tmp.empty) {
            races[i] = tmp;
            if (race_id == tmp.id) {
                result.n = i;
            }
            i++;
        }
    }
    return result;
} 

t_race* get_s_ui(int all_races) {
    t_season *season = get_m_ui();
    if (season == NULL) {
        return NULL;
    }
    int race_id;
    if (all_races) {
        race_id = 0;
    } else {
        printf("enter race id: ");
        scanf("%d", &race_id);
        if (race_id < 1) {
            printf("this race doesn`t exist.\n");
            return NULL;
        }
    }

    array_pair result = get_s(season, race_id);

    if (result.n == -1) {
        printf("this race doesn`t exist.\n");
        return NULL;
    }
    if (!all_races) {
        int ind = result.n;
        printf("-----------\n");
        printf("race id: %d\nname: %s\nlap count: %d\nlap length: %d\n", 
        result.array[ind].id, result.array[ind].name, result.array[ind].lap_count, result.array[ind].lap_length);
        return &result.array[ind];
    }

    if (result.n == 0) {
        printf("this season doesn`t have races.\n");
        return NULL;
    }
    for (int i = 0; i < result.n; i++) {
        printf("-----------\n");
        printf("race id: %d\nname: %s\nlap count: %d\nlap length: %d\n", 
        result.array[i].id, result.array[i].name, result.array[i].lap_count, result.array[i].lap_length);
    }
    return NULL;
}

void update_m(t_season* season) {
    fseek(fseasons_ind, sizeof(int)*(season->id-1), SEEK_SET);
    int offset;
    fread(&offset, sizeof(int), 1, fseasons_ind);
    fseek(fseasons, offset, SEEK_SET);
    fwrite(season, sizeof(t_season), 1, fseasons);
}

void update_m_ui() {
    t_season *season = get_m_ui();
    if (season == NULL) {
        return;
    }
    printf("choose field to change (1: year, 2: race director, 3: tyre supplier)\n");
    int command;
    scanf("%d", &command);
    switch (command)
    {
    case 1:
        printf("enter year: ");
        scanf("%d", &(season->year));
        break;
    case 2:
        printf("enter race director: ");
        scanf("%s", season->race_director);
        break;
    case 3:
        printf("enter tyre supplier: ");
        scanf("%s", season->tyre_supplier);
        break;
    default:
        printf("invalid command.");
        return;
    }
    update_m(season);
    printf("done.\n");
}

void update_s_ui() {
    t_race *race = get_s_ui(0);
    if (race == NULL) {
        return;
    }
    printf("choose field to change (1: name, 2: lap count, 3: lap length)\n");
    int command;
    scanf("%d", &command);
    switch (command)
    {
    case 1:
        printf("enter name: ");
        scanf("%s", race->name);
        break;
    case 2:
        printf("enter lap count: ");
        scanf("%d", &(race->lap_count));
        break;
    case 3:
        printf("enter lap length: ");
        scanf("%d", &(race->lap_length));
        break;
    default:
        printf("invalid command.");
        return;
    }
    fseek(fraces, race->offset, SEEK_SET);
    fwrite(race, sizeof(t_race), 1, fraces);
    printf("done.\n");
}

void insert_m_ui(int* garbage_buffer, int* n) {
    t_season season;
    printf("enter season year, race director and tyre supplier:\n");
    scanf("%d %s %s", &season.year, season.race_director, season.tyre_supplier);
    fseek(fseasons_ind, 0L, SEEK_END);
    season.id = ftell(fseasons_ind) / sizeof(int) + 1;
    season.races_cnt = 0;
    season.first_race = -1;
    if (*n > 0) {
        (*n)--;
        fseek(fseasons, garbage_buffer[*n], SEEK_SET);

        t_season tmp;
        fread(&tmp, sizeof(t_season), 1, fseasons);
        season.first_race = tmp.first_race;
        fseek(fseasons, garbage_buffer[*n], SEEK_SET);
    } else {
        fseek(fseasons, 0L, SEEK_END);
    }
    int offset = ftell(fseasons);
    fwrite(&season, sizeof(t_season), 1, fseasons);
    fwrite(&offset, sizeof(int), 1, fseasons_ind);
    printf("season added with id %d.\n", season.id);
}

t_race* first_free_chunk(t_season* season) {
    int next_race = season->first_race;
    t_race* tmp = (t_race*)malloc(sizeof(t_race));
    while (next_race != -1) {
        fseek(fraces, next_race, SEEK_SET);
        fread(tmp, sizeof(t_race), 1, fraces);
        next_race = tmp->next_race;
        if (tmp->empty) {
            return tmp;
        }
    }
    return NULL;
}

void insert_s_ui() {
    int season_id;
    printf("enter season id: ");
    scanf("%d", &season_id);
    t_season* season = get_m(season_id);
    if (season == NULL) {
        printf("this season doesn`t exist.");
        return;
    }

    t_race race;
    fseek(fraces, 0L, SEEK_END);
    race.offset = ftell(fraces);
    race.season_id = season_id;
    race.next_race = season->first_race;
    if (season->first_race == -1) {
        race.id = 1;
        season->first_race = race.offset;
    } else {
        t_race* free_chunk = first_free_chunk(season);
        if (free_chunk == NULL) {
            int id;
            fseek(fraces, season->first_race, SEEK_SET);
            fread(&id, sizeof(int), 1, fraces);
            race.id = id + 1;
            season->first_race = race.offset;
        } else {
            race = *free_chunk;
        }
    }
    race.empty = 0;

    printf("enter race name, lap count and lap length:\n");
    scanf("%s %d %d", race.name, &race.lap_count, &race.lap_length);

    fseek(fraces, race.offset, SEEK_SET);
    fwrite(&race, sizeof(t_race), 1, fraces);

    season->races_cnt++;
    update_m(season);

    printf("race added with id %d.\n", race.id);
}

void del_s(t_race *race) {
    race->empty = 1;
    fseek(fraces, race->offset, SEEK_SET);
    fwrite(race, sizeof(t_race), 1, fraces);
    t_season *season = get_m(race->season_id);
    season->races_cnt--;
    update_m(season);
}

void del_m_ui(int* garbage_buffer, int* n) {
    t_season *season = get_m_ui();
    if (season == NULL) {
        return;
    }
    array_pair races = get_s(season, 0);
    for (int i = 0; i < races.n; i++) {
        del_s(&races.array[i]);
    }

    int tmp;
    fseek(fseasons_ind, sizeof(int)*(season->id-1), SEEK_SET);
    fread(&tmp, sizeof(int), 1, fseasons_ind);
    if (*n < GARBAGE_BUFFER_LEN) {
        garbage_buffer[*n] = tmp;
        (*n)++;
    }

    tmp = -1;
    fseek(fseasons_ind, sizeof(int)*(season->id-1), SEEK_SET);
    fwrite(&tmp, sizeof(int), 1, fseasons_ind);
}

void del_s_ui() {
    t_race *race = get_s_ui(0);
    if (race == NULL) {
        return;
    }
    del_s(race);
}

void count() {
    int cnt_seasons = 0;
    int cnt_races = 0;
    fseek(fseasons_ind, 0L, SEEK_END);
    int end = ftell(fseasons_ind)/sizeof(int);
    int id = 1;
    for (int id = 1; id <= end; id++) {
        t_season *season = get_m(id);
        if (season != NULL) {
            cnt_seasons++;
            cnt_races += season->races_cnt;
        }
    }
    printf("seasons: %d\nraces: %d\n", cnt_seasons, cnt_races);
}

int read_garbage_buffer(int* garbage_buffer) {
    fseek(fgarbage, 0L, SEEK_END);
    int n = ftell(fgarbage)/sizeof(int);
    n = n < GARBAGE_BUFFER_LEN ? n : GARBAGE_BUFFER_LEN;
    fseek(fgarbage, 0L, SEEK_SET);
    fread(garbage_buffer, sizeof(int), n, fgarbage);
    return n;
}

int main() {
    fraces = fopen("races.fl", "a+");
    fseasons = fopen("seasons.fl", "a+");
    fseasons_ind = fopen("seasons.ind", "a+");
    fclose(fraces);
    fclose(fseasons);
    fclose(fseasons_ind);
    fraces = fopen("races.fl", "r+");
    fseasons = fopen("seasons.fl", "r+");
    fseasons_ind = fopen("seasons.ind", "r+");

    int garbage_buffer[GARBAGE_BUFFER_LEN];
    fgarbage = fopen("garbage.fl", "a+");
    int cur_buffer_len = read_garbage_buffer(garbage_buffer);
    fclose(fgarbage);
    fgarbage = fopen("garbage.fl", "w");

    while (1) {
        int command;
        printf("-----------\n");
        printf("choose command:\n0: exit\n1: get season\n2: get race\n3: del season\n4: del race\n");
        printf("5: update season\n6: update race\n7: insert season\n8: insert race\n9: info\n");
        scanf("%d", &command);
        switch (command) {
            case 0:
                fclose(fraces);
                fclose(fseasons);
                fclose(fseasons_ind);
                fwrite(garbage_buffer, sizeof(int), cur_buffer_len, fgarbage);
                fclose(fgarbage);
                return 0;
            case 1:
                get_m_ui();
                break;
            case 2:
                printf("print concrete race(0) or all races(1)? ");
                int all_races;
                scanf("%d", &all_races);
                get_s_ui(all_races);
                break;
            case 3:
                del_m_ui(garbage_buffer, &cur_buffer_len);
                break;
            case 4:
                del_s_ui();
                break;
            case 5:
                update_m_ui();
                break;
            case 6:
                update_s_ui();
                break;
            case 7:
                insert_m_ui(garbage_buffer, &cur_buffer_len);
                break;
            case 8:
                insert_s_ui();
                break;
            case 9:
                count();
                break;
            default:
                printf("invalid command.");
                break;
        }
    }
    return 0;
}
