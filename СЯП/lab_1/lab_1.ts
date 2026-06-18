export {};

interface User {
    name: string;
    age: number;
}

interface UserLocation {
    city: string;
    country: string;
}

interface UserWithLocation extends User {
    location: UserLocation;
}

interface UserWithSkills extends User {
    skills: string[];
}

interface SimpleExams {
    maths: boolean;
    programming: boolean;
}

interface User4Type extends User {
    studies: {
        university: string;
        speciality: string;
        year: number;
        exams: SimpleExams;
    };
}

interface Article {
    title: string;
    pagesNumber: number;
}

interface Professor {
    name: string;
    degree: string;
    articles?: Article[];
}

interface ComplexExam {
    maths?: boolean;
    programming?: boolean;
    mark: number;
    professor?: Professor;
}

interface UserComplex extends User {
    studies: {
        university: string;
        speciality: string;
        year: number;
        department: {
            faculty: string;
            group: number;
        };
        exams: ComplexExam[];
    };
}

interface Post { 
    id: number; 
    message: string; 
    likesCount: number; 
}
interface Dialog { 
    id: number; 
    name: string; 
}
interface Message { 
    id: number; 
    message: string; 
}

interface Store {
    state: {
        profilePage: { posts: Post[]; newPostText: string; };
        dialogsPage: { dialogs: Dialog[]; messages: Message[]; };
        sidebar: any[];
    };
}

let user: User = { name: 'Masha', age: 21 };
let numbers: number[] = [1, 2, 3];

let user1: UserWithLocation = {
    name: 'Masha', age: 23,
    location: { city: 'Minsk', country: 'Belarus' }
};

let user2: UserWithSkills = {
    name: 'Masha', age: 28,
    skills: ["HTML", "CSS", "JavaScript", "React"]
};

type arr = { 
    id1: number;
}

type aaa ={
    name: string
}

type num = arr & aaa

const efg: num = {
    id1: 11,
    name: "fghj",
}

let _arr:arr = {
    id1: 10
};

const array: {id: number, name: string, group: number}[] = [
    {id: 1, name: 'Vasya', group: 10},
    {id: 2, name: 'Ivan', group: 11},
    {id: 3, name: 'Masha', group: 12},
    {id: 4, name: 'Petya', group: 10},
    {id: 5, name: 'Kira', group: 11}
];

let user4: User4Type = {
    name: 'Masha', age: 19,
    studies: {
        university: 'BSTU', speciality: 'designer', year: 2020,
        exams: { maths: true, programming: false }
    }
};

let user5: UserComplex = {
    name: 'Masha', age: 22,
    studies: {
        university: 'BSTU', speciality: 'designer', year: 2020,
        department: { faculty: 'FIT', group: 10 },
        exams: [
            { maths: true, mark: 8 },
            { programming: true, mark: 4 }
        ]
    }
};

let user6: UserComplex = {
    name: 'Masha', age: 21,
    studies: {
        university: 'BSTU', speciality: 'designer', year: 2020,
        department: { faculty: 'FIT', group: 10 },
        exams: [
            { maths: true, mark: 8, professor: { name: 'Ivan Ivanov', degree: 'PhD' } },
            { programming: true, mark: 10, professor: { name: 'Petr Petrov', degree: 'PhD' } }
        ]
    }
};

let user7: UserComplex = {
    name: 'Masha', age: 20,
    studies: {
        university: 'BSTU', speciality: 'designer', year: 2020,
        department: { faculty: 'FIT', group: 10 },
        exams: [
            {
                maths: true, mark: 8,
                professor: {
                    name: 'Ivan Petrov', degree: 'PhD',
                    articles: [
                        { title: "About HTML", pagesNumber: 3 },
                        { title: "About CSS", pagesNumber: 5 },
                        { title: "About JavaScript", pagesNumber: 1 }
                    ]
                }
            },
            {
                programming: true, mark: 10,
                professor: {
                    name: 'Petr Ivanov', degree: 'PhD',
                    articles: [
                        { title: "About HTML", pagesNumber: 3 },
                        { title: "About CSS", pagesNumber: 5 },
                        { title: "About JavaScript", pagesNumber: 1 }
                    ]
                }
            }
        ]
    }
};

let store: Store = {
    state: {
        profilePage: {
            posts: [
                { id: 1, message: 'Hi', likesCount: 12 },
                { id: 2, message: 'By', likesCount: 1 }
            ],
            newPostText: 'About me'
        },
        dialogsPage: {
            dialogs: [
                { id: 1, name: 'Valera' },
                { id: 2, name: 'Andrey' },
                { id: 3, name: 'Sasha' },
                { id: 4, name: 'Viktor' }
            ],
            messages: [
                { id: 1, message: 'hi' },
                { id: 2, message: 'hi hi' },
                { id: 3, message: 'hi hi hi' }
            ]
        },
        sidebar: []
    }
};

let userCopy: User = { ...user };
let numbersCopy: number[] = [...numbers];
let user1Copy: UserWithLocation = { ...user1, location: { ...user1.location } };
let user2Copy: UserWithSkills = { ...user2, skills: [...user2.skills] };

let arrayCopy = array.map(item => ({ ...item }));

let user4Copy: User4Type = {
    ...user4,
    studies: {
        ...user4.studies,
        exams: { ...user4.studies.exams }
    }
};

let user5Copy: UserComplex = {
    ...user5,
    studies: {
        ...user5.studies,
        department: { ...user5.studies.department },
        exams: user5.studies.exams.map(exam => ({ ...exam }))
    }
};

let user6Copy: UserComplex = {
    ...user6,
    studies: {
        ...user6.studies,
        department: { ...user6.studies.department },
        exams: user6.studies.exams.map(exam => ({
            ...exam,
            professor: exam.professor ? { ...exam.professor } : undefined
        }))
    }
};

let user7Copy: UserComplex = {
    ...user7,
    studies: {
        ...user7.studies,
        department: { ...user7.studies.department },
        exams: user7.studies.exams.map(exam => ({
            ...exam,
            professor: exam.professor ? {
                ...exam.professor,
                articles: exam.professor.articles ? exam.professor.articles.map(article => ({ ...article })) : undefined
            } : undefined
        }))
    }
};

let copyUser7: UserComplex = { 
    ...user7, 
    studies: {
        ...user7.studies, 
        department: { ...user7.studies.department },
        exams: user7.studies.exams.map(exam => ({
            ...exam, 
            professor: exam.professor ? {
                ...exam.professor, 
                articles: exam.professor.articles ? exam.professor.articles.map(article => ({ ...article })) : undefined
            } : undefined
        }))
    }  
};

let storeCopy: Store = {
    ...store,
    state: {
        ...store.state,
        profilePage: {
            ...store.state.profilePage,
            posts: store.state.profilePage.posts.map(post => ({ ...post, message: "Hello" }))
        },
        dialogsPage: {
            ...store.state.dialogsPage,
            messages: store.state.dialogsPage.messages.map(msg => ({ ...msg, message: "Hello" }))
        }
    }
};

user5Copy.studies.department.group = 12;

const exam5 = user5Copy.studies.exams.find(exam => exam.programming);
if (exam5) exam5.mark = 10;

const exam6 = user6Copy.studies.exams.find(exam => exam.programming);
if (exam6 && exam6.professor) exam6.professor.name = 'New Professor Name';

const exam7 = user7Copy.studies.exams.find(exam => 
    exam.professor && exam.professor.name === 'Petr Ivanov'
);
if (exam7 && exam7.professor && exam7.professor.articles) {
    const art = exam7.professor.articles.find(article => article.title === 'About CSS');
    if (art) art.pagesNumber = 3;
}

console.log(userCopy);

//Ctrl + ~
//tsc lab_1.ts --target es6
//node lab_1.js